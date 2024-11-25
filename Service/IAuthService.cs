using AutoRepairMainCore.Entity.ServiceFolder;
namespace AutoRepairMainCore.Service
{
    public interface IAuthService
    {
        Task<string> RegisterServiceAsync(string serviceName, string password);
        Task<string> LoginServiceAsync(string serviceName, string password);
        Task<string> GenerateJwtTokenAsync(MyService myService);
    }
}
