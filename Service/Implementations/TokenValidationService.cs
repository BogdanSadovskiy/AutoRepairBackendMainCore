using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoRepairMainCore.Service.Implementations
{
    public class TokenValidationService : ITokenValidationService
    {
        private readonly IConfiguration _configuration;

        public TokenValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AutoService AutoService, Role role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, AutoService.Name),
                new Claim(ClaimTypes.Role, role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(48),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            IsTokenEmpty(token); 
            token = RemovePrefixOfToken(token);

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                tokenHandler.ValidateToken(token, validationParameters, out _);
                return true; 
            }
            catch
            {
                throw new SecurityTokenException("Authorization is not ok");
            }
        }

        private string RemovePrefixOfToken(string token)
        {
            if (token.StartsWith("Bearer "))
            {
                return token.Substring(7);
            }
            return token;
        }

        private bool IsTokenEmpty(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException();
            }
            return false;
        }
    }
    
}
