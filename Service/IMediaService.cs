using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IMediaService
    {
        Task<string> SaveAutoServiceLogo(AutoService autoService, IFormFile logoFile);
    }
}
