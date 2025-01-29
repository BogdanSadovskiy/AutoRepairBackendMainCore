using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairMainCore.Service.Implementations
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private MySqlContext _context;

        public UserService(IConfiguration configuration, MySqlContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public AutoService CreateAutoService(string name, string password)
        {
            return  new AutoService() 
            {
                Name = name, 
                Password = password
            };
        }

        public async Task<AutoService> GetAutoServiceByName(string autoserviceName)
        {
            AutoService autoService = await _context.services.FirstOrDefaultAsync(s => s.Name == autoserviceName);
            return autoService;
        }

        public void UpdateAutoServiceLogoPath(AutoService autoService,  string logoPath)
        {
            autoService.serviceIconFilePath = logoPath;
            _context.services.Update(autoService);
            _context.SaveChanges();
        }

        public async Task<AutoService> GetAutoServiceById(int id)
        {
            AutoService autoService = await _context.services.FirstOrDefaultAsync(s => s.Id == id);
            return autoService;
        }
    }
}
