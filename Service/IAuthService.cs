using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
namespace AutoRepairMainCore.Service
{
    public interface IAuthService
    {
        Task<string> RegisterServiceAsync(AutoServiceAuthDto userService);
        Task<string> LoginServiceAsync(AutoServiceAuthDto userService);
        Task<string> GenerateJwtTokenAsync(AutoService myService, Role role);
    }
}
