using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoRepairMainCore.DTO.Models;

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

        public bool ValidateToken(string token, RolesEnum expectedRole)
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

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                CheckRole(principal, expectedRole);

                return true; 
            }
            catch(SecurityTokenException e)
            {
                throw new SecurityTokenException(e.Message);
            }
        }

        private void CheckRole(ClaimsPrincipal principal, RolesEnum expectedRole)
        {
            if (expectedRole == RolesEnum.anyone) 
            {
                return; 
            }
 
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim == null)
            {
                throw new SecurityTokenException("Token does not contain a role claim."); //буде "access denied", але тимчасово є так
            }

            if (expectedRole != Role.getEnumRole(roleClaim.Value))
            {
                throw new SecurityTokenException($"Wrong Role.");
            }
        }

        public string GetAutoServiceNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            string autoServiceName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return autoServiceName;
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
