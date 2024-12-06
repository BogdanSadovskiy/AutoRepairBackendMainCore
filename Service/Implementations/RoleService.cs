using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Infrastructure;

namespace AutoRepairMainCore.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private IConfiguration _configuration;
        private MySqlContext _context;
        public RoleService(IConfiguration configuration, MySqlContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Role GetRole(int roleId)
        {

            Role role = _context.roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }

            return role;
        }
    }
}
