using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.ServiceFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity
{
    public class Role
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public ICollection<AutoService> AutoServices { get; set; } = new List<AutoService>();

        public static RolesEnum? getEnumRole(string role)
        {
            if (role == "admin")
            {
                return RolesEnum.Admin;
            }
            else if (role == "user")
            {
                return RolesEnum.User;
            }
            return null;
        }

        public static int setAdminRole()
        {
            return (int)RolesEnum.Admin;
        }

        public static int setUserRole()
        {
            return (int)RolesEnum.User;
        }

    }
}
