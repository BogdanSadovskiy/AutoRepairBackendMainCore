using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IRoleService
    {
        Role GetRole(int roleId);

        void SetRole(AutoService autoService);
    }
}
