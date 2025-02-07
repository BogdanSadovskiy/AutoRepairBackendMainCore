using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IUserService
    {
        AutoService CreateAutoService(string name, string password);

        Task<AutoService> GetAutoServiceByName(string autoserviceName);

        Task<AutoService> GetAutoServiceById(int id);

        void UpdateAutoServiceLogoPath(AutoService autoService, string logoPath);
    }
}
