using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IUserService
    {
        AutoService CreateAutoServiceObject(string name, string email, string password);

        Task<AutoService> GetAutoServiceByEmail(string email);

        Task<AutoService> GetAutoServiceById(int id);

        void UpdateAutoServiceLogoPath(AutoService autoService, string logoPath);
    }
}
