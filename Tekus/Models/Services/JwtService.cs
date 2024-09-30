using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TekusApi.Services
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly double _expirationInMinutes;
        public JwtService(IConfiguration config)
        {
            _secret = config["JwtConfig:Secret"]?? throw new TekusException("Secret must be not null");
            _expirationInMinutes = double.Parse(config["JwtConfig:ExpirationInMinutes"]??"60");
        }
        public string GenerateSecurityToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, username)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}