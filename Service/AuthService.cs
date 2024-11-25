using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoRepairMainCore.Service
{

    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private DbContext _context;

        public AuthService(IConfiguration configuration, DbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Register service with hashed password
        public async Task<string> RegisterServiceAsync(string serviceName, string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var myService = new MyService
            {
                service_name = serviceName,
                service_password = hashedPassword,
            };

            _context.Services.Add(myService);
            await _context.SaveChangesAsync();

            return "Service registered successfully!";
        }

        public async Task<string> LoginServiceAsync(string serviceName, string password)
        {
            var myService = await _context.Services.Include(s => s.role)
                                                 .FirstOrDefaultAsync(s => s.service_name == serviceName);
            if (myService == null)
                return "Invalid service name or password";

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, myService.service_password);
            if (!isPasswordValid)
                return "Invalid service name or password";

            var token = await GenerateJwtTokenAsync(myService);
            return token;
        }

        // Generate JWT token
        public async Task<string> GenerateJwtTokenAsync(MyService myService)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, myService.service_name),
            new Claim(ClaimTypes.Role, myService.role.role_name),  
            // Add other claims if necessary, such as service ID, etc.
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),  // Token expiration time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }







    }

}

