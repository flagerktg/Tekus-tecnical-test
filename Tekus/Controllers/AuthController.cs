using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;
using TekusApi.Services;
namespace TekusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        // Almacenamiento de usuarios en memoria
        private readonly Dictionary<string, string> _usuarios = new Dictionary<string, string>
        {
            { "tekus1", "password1" },
            { "tekus2", "password2" }
        };

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Mensaje = "Username and password are required." });
            }
            if (_usuarios.ContainsKey(request.Username) && _usuarios[request.Username] == request.Password)
            {
                var token = _jwtService.GenerateSecurityToken(request.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized(new { Mensaje = "Invalid credentials." });
        }

    }
}