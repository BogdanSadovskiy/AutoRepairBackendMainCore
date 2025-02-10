using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface ITokenValidationService
    {
        string GenerateToken(AutoService AutoService, Role role);

        int GetAutoServiceIdFromToken(string token);
    }
}
