using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.DTO.Models;

namespace AutoRepairMainCore.Service
{
    public interface ITokenValidationService
    {
        string GenerateToken(AutoService AutoService, Role role);
        int GetAutoServiceIdFromToken(string token);
    }
}
