using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;
namespace AutoRepairMainCore.Service
{
    public interface IAuthService
    {
        Task<string> RegisterServiceAsync(MyServiceRegistrationDto userService);
        Task<string> LoginServiceAsync(MyServiceLoginDto userService);
        Task<string> GenerateJwtTokenAsync(MyService myService, Role role);
    }
}
