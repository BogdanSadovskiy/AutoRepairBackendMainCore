using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AutoRepairMainCore.Service.Implementations
{

    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private MySqlContext _context;
        private ITokenValidationService _tokenValidationService;
        private IRoleService _roleService;
        private IUserService _userService;


        public AuthService(IConfiguration configuration, MySqlContext context, IRoleService roleService, 
            ITokenValidationService tokenValidationService, IUserService userService)
        {
            _configuration = configuration;
            _context = context;
            _roleService = roleService;
            _tokenValidationService = tokenValidationService;
            _userService = userService;
        }


        public async Task<string> RegisterServiceAsync(AutoServiceAuthDto userAutoService)
        {
            ValidatePassword(userAutoService.Password);

            if (await _userService.GetAutoServiceByName(userAutoService.Name) != null)
            {
                throw new AutoServiceAlreadyExistException($"A service \"{userAutoService.Name}\" already exists.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userAutoService.Password);

            AutoService myService = _userService.CreateAutoService(userAutoService.Name, hashedPassword);
            _roleService.SetRole(myService);
      
            _context.services.Add(myService);
            await _context.SaveChangesAsync();
            return $"Service {myService.Name} registered successfully!";
        }

        public async Task<AutoServiceFrontendDTO> LoginServiceAsync(AutoServiceAuthDto userAutoService)
        {
            AutoService autoService = await _userService.GetAutoServiceByName(userAutoService.Name);
            if (autoService == null)
            {
                throw new AutoServiceNotFoundException("Invalid service name or password");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userAutoService.Password, autoService.Password);
            if (!isPasswordValid)
            {
                throw new AutoServiceNotFoundException("Invalid service name or password");
            }
            Role role = _roleService.GetRole(autoService.RoleId);
            string token = _tokenValidationService.GenerateToken(autoService, role);

            return IfSuccessfullLogin(autoService, token);
        }

        private AutoServiceFrontendDTO IfSuccessfullLogin(AutoService autoservice, string token)
        {
            AutoServiceFrontendDTO autoServiceFrontendDTO = new AutoServiceFrontendDTO()
            {
                Name = autoservice.Name,
                Role = autoservice.Role.Name,
                LogoPath = autoservice.serviceIconFilePath,
                Token = token
            };
            return autoServiceFrontendDTO;
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) ||
                password.Length < 8 ||

                !Regex.IsMatch(password, @"[A-Z]"))
            {
                string passwordRule = "Password has to be:\n" +
                                    "At least 8 characters long.\n" +
                                    "At least one uppercase letter.";

                throw new PasswordValidateException(passwordRule);
            }
        }
    }
}
