﻿using AutoRepairMainCore.DTO;
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
        IRoleService _roleService;


        public AuthService(IConfiguration configuration, MySqlContext context, 
            IRoleService roleService, ITokenValidationService tokenValidationService)
        {
            _configuration = configuration;
            _context = context;
            _roleService = roleService;
            _tokenValidationService = tokenValidationService;
        }


        public async Task<string> RegisterServiceAsync(AutoServiceAuthDto userAutoService)
        {
            ValidatePassword(userAutoService.Password);

            if (await GetExistingService(userAutoService.Name) != null)
            {
                throw new AutoServiceAlreadyExistException($"A service \"{userAutoService.Name}\" already exists.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userAutoService.Password);

            AutoService myService = new AutoService
            {
                Name = userAutoService.Name,
                Password = hashedPassword,
                RoleId = Role.setUserRole()
            };

            _context.services.Add(myService);
            await _context.SaveChangesAsync();
            return $"Service {myService.Name} registered successfully!";
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
                throw new AutoServiceNotFoundException("Invalid service name or password");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userAutoService.Password, autoService.Password);
            if (!isPasswordValid)
            {
                throw new AutoServiceNotFoundException("Invalid service name or password");
            }
            Role role = _roleService.GetRole(autoService.RoleId);
            string token = _tokenValidationService.GenerateToken(autoService, role);
            return token;
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
