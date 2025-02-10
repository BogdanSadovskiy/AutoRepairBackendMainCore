using AutoRepairMainCore.DTO;
namespace AutoRepairMainCore.Service
{
    public interface IAuthService
    {
        Task<string> RegisterServiceAsync(AutoServiceAuthDto userService);

        Task<AutoServiceFrontendDTO> LoginServiceAsync(AutoServiceAuthDto userService);
    }
}
