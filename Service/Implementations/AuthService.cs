using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoRepairMainCore.Service.Implementations
{

    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private MySqlContext _context;
        IRoleService _roleService;


        public AuthService(IConfiguration configuration, MySqlContext context, IRoleService roleService)
        {
            _configuration = configuration;
            _context = context;
            _roleService = roleService;
        }


        public async Task<string> RegisterServiceAsync(AutoServiceAuthDto userAutoService)
        {
            if (await GetExistingService(userAutoService.Name) != null)
            {
                throw new InvalidOperationException($"A service \"{userAutoService.Name}\" already exists.");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userAutoService.Password).ToString();

            AutoService myService = new AutoService
            {
                Name = userAutoService.Name,
                Password = hashedPassword,
                RoleId = Role.setUserRole()
            };

            _context.services.Add(myService);
            await _context.SaveChangesAsync();

            return "Service registered successfully!";
        }

        private async Task<AutoService> GetExistingService(string AutoServiceName)
        {
            return await _context.services.FirstOrDefaultAsync(s => s.Name == AutoServiceName);
        }

        public async Task<string> LoginServiceAsync(AutoServiceAuthDto userAutoService)
        {
            AutoService autoService = await GetExistingService(userAutoService.Name);
            if (autoService == null)
            {
                throw new InvalidOperationException("Invalid service name or password");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userAutoService.Password, autoService.Password);
            if (!isPasswordValid)
            {
                throw new InvalidOperationException("Invalid service name or password");
            }
            Role role = _roleService.GetRole(autoService.RoleId);
            string token = await GenerateJwtTokenAsync(autoService, role);
            return token;
        }


        public async Task<string> GenerateJwtTokenAsync(AutoService AutoService, Role role)
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
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
