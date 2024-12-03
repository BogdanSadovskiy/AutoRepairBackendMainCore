using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
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
        private MySqlContext _context;
        IRoleService _roleService;
        

        public AuthService(IConfiguration configuration, MySqlContext context, IRoleService roleService)
        {
            _configuration = configuration;
            _context = context;
            _roleService = roleService;
        }

     
        public async Task<string> RegisterServiceAsync(MyServiceRegistrationDto userService)
        {
            if(await CheckExistingService(userService.ServiceName) != null)
                throw new InvalidOperationException($"A service \"{userService.ServiceName}\" already exists.");
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userService.Password).ToString();

            MyService myService = new MyService
            {
                service_name = userService.ServiceName,
                service_password = hashedPassword,
                role_id = Role.setUserRole()
            };

            _context.services.Add(myService);
            await _context.SaveChangesAsync();

            return "Service registered successfully!";
        }

        private async Task<MyService> CheckExistingService(string serviceName)
        {
            return  await _context.services.FirstOrDefaultAsync(s => s.service_name == serviceName);
        }

        public async Task<string> LoginServiceAsync(MyServiceLoginDto userService)
        {
            MyService myService = await (CheckExistingService(userService.ServiceName));
            if (myService == null)
            {
                return "Invalid service name or password";
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userService.Password, myService.service_password);
            if (!isPasswordValid)
            {
                return "Invalid service name or password";
            }
            Role role = _roleService.GetRole(myService.role_id);
            string token = await GenerateJwtTokenAsync(myService, role);
            return token;
        }

       
        public async Task<string> GenerateJwtTokenAsync(MyService myService,Role role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, myService.service_name),
                new Claim(ClaimTypes.Role, role.role_name),  
       
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

