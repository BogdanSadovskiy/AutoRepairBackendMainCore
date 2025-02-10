using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IMediaService
    {
        Task<string> SaveAutoServiceLogo(AutoService autoService, IFormFile logoFile);

        Task<string> SaveEmployeePhoto(AutoService autoService, Employee employee, IFormFile photo);
    }
}
