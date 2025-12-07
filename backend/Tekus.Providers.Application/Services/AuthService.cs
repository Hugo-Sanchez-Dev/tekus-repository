#region Usings
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tekus.Providers.Application.Interfaces;
#endregion

namespace Tekus.Providers.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Constants
        private const string NAME = "Hugo Sánchez";
        private const string EMAIL = "sanchez.anturi@gmail.com";
        private const string ROLE = "Admin";
        #endregion

        #region Instances
        private readonly IConfiguration _configuration;
        #endregion

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken()
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(signingCredentials);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, NAME),
                new Claim(ClaimTypes.Email, EMAIL),
                new Claim(ClaimTypes.Role, ROLE)
            };

            JwtPayload payload = new JwtPayload
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(1)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
