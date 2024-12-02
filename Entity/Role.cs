using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string role_name { get; set; }
        public ICollection<MyService> Services { get; set; } = new List<MyService>();

        public RolesEnum getEnumRole()
        {
            return role_name == "admin" ? RolesEnum.admin :
          (role_name == "user" ? RolesEnum.user : 
          throw new ArgumentException("Invalid role name"));
        }
        public static int setAdminRole()
        {
            return (int)RolesEnum.admin;
        }
        public static int setUserRole()
        {
            return ((int)RolesEnum.user);
        }

    }
}
