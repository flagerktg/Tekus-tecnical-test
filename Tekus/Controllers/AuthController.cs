using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TekusApi.Models;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for handling authentication requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="securityService">The security service.</param>
        /// <param name="logger">The logger.</param>
        public AuthController(IConfiguration configuration, ISecurityService securityService, ILogger<AuthController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates an Access Token.
        /// </summary>
        /// <param name="model">Credentials of user for which access token must be created.</param>
        /// <returns>Access Token of user provided, when credentials are valid.</returns>
        /// <response code="200">Access Token of user provided, when credentials are valid.</response>
        /// <response code="401">Credentials are invalid.</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(AuthResult))]
        [ProducesResponseType(401, Type = typeof(ApiError))]
        public ActionResult<AuthResult> CreateToken(AuthRequest model)
        {
            if (model == null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                return Unauthorized(new ApiError { Message = "Invalid credentials" });
            }

            try
            {
                UserDto userDto = _securityService.CheckCredentials(model.Login, model.Password);

                if (userDto == null || userDto.Id == 0)
                {
                    return Unauthorized(new ApiError { Message = "Invalid credentials" });
                }

                var token = GenerateJWT(userDto);

                return Ok(new AuthResult
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating token for user with login {Login}.", model.Login);
                return Unauthorized(new ApiError { Message = "Invalid credentials" });
            }
        }

        /// <summary>
        /// Generates a JWT for a user with a set of permissions.
        /// </summary>
        /// <param name="user">The user for whom the JWT must be generated.</param>
        /// <returns>JWT generated.</returns>
        private string GenerateJWT(UserDto? user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "User ID cannot be null or empty.");
            }

            var jwtKey = _configuration["Auth:Jwt:Keys"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey.Split(",")[0]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Auth:Jwt:Issuer"],
                audience: _configuration["Auth:Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Auth:Jwt:ExpirationMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
