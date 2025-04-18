using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Infrastructure;
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
            ValidateEmail(userAutoService.Email);
            ValidatePassword(userAutoService.Password);
            ValidateName(userAutoService.Name);

            if (await _userService.GetAutoServiceByEmail(userAutoService.Email) != null)
            {
                throw new AutoServiceAlreadyExistException($"This Email already used.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userAutoService.Password);

            AutoService myService = _userService.CreateAutoServiceObject(userAutoService.Name, userAutoService.Email, hashedPassword);
            _roleService.SetRole(myService);

            _context.services.Add(myService);
            await _context.SaveChangesAsync();
            return $"Service {myService.Name} registered successfully!";
        }

        public async Task<AutoServiceFrontendDTO> LoginServiceAsync(AutoServiceAuthDto userAutoService)
        {
            AutoService autoService = await _userService.GetAutoServiceByEmail(userAutoService.Email);
            if (autoService == null)
            {
                throw new AutoServiceNotFoundException("Invalid service email or password");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userAutoService.Password, autoService.Password);
            if (!isPasswordValid)
            {
                throw new AutoServiceNotFoundException("Invalid service name or password");
            }
            Role role = _roleService.GetRole(autoService.RoleId);
            string token = _tokenValidationService.GenerateToken(autoService, role);

            return SuccessfullLogin(autoService, token);
        }

        private AutoServiceFrontendDTO SuccessfullLogin(AutoService autoservice, string token)
        {
            AutoServiceFrontendDTO autoServiceFrontendDTO = new AutoServiceFrontendDTO()
            {
                Id = autoservice.Id,
                Email = autoservice.Email,
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

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) ||
                !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) {
                throw new InvalidParameterException("Input correct email type");
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidParameterException("Input Name of your autoservice");
            }
        }
    }
}
