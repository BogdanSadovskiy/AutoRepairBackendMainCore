using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoRepairMainCore.Service.Implementations
{
    public class TokenValidationService : ITokenValidationService
    {
        private readonly IConfiguration _configuration;
        private int expiryHours;

        public TokenValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
            expiryHours = 24;
        }

        public string GenerateToken(AutoService AutoService, Role role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, AutoService.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name)
            };

            string secretKey = _configuration["Jwt:Key"];
            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int GetAutoServiceIdFromToken(string token)
        {
            token = RemoveBearerFromToken(token);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenS = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;

            if (tokenS == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            int idClaim = int.Parse(tokenS?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idClaim == null)
            {
                throw new UnauthorizedAccessException("Token does not contain necessary claims.");
            }
            return idClaim;
        }

        private string RemoveBearerFromToken(string token)
        {
            string tokenWithoutBearer = token;
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                tokenWithoutBearer = token.Substring("Bearer ".Length).Trim();
            }
            return tokenWithoutBearer;
        }
    }
}
