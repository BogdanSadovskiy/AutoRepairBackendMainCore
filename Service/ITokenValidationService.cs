using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Entity;
using AutoRepairMainCore.DTO.Models;

namespace AutoRepairMainCore.Service
{
    public interface ITokenValidationService
    {
        bool ValidateToken(string token, RolesEnum expectedRole);
        string GenerateToken(AutoService AutoService, Role role);
        string GetAutoServiceNameFromToken(string token);
    }
}
