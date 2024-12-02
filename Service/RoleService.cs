using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Infrastructure;

namespace AutoRepairMainCore.Service
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

        public Role GetRole(int role_id)
        {
           
            Role role = _context.roles.FirstOrDefault(r => r.id == role_id);

            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {role_id} not found.");
            }

            return role;
        }
    }
}
