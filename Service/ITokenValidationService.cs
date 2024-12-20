using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Entity;

namespace AutoRepairMainCore.Service
{
    public interface ITokenValidationService
    {
        bool ValidateToken(string token);
        string GenerateToken(AutoService AutoService, Role role);
    }
}
