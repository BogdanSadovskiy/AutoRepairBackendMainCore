using AutoRepairMainCore.DTO;
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

        public RolesEnum? getEnumRole()
        {
            if (this.Name == "admin")
            {
                return RolesEnum.admin;
            }
            else if (this.Name == "user")
            {
                return RolesEnum.user;
            }
            return null;
        }

        public static int setAdminRole()
        {
            return (int)RolesEnum.admin;
        }

        public static int setUserRole()
        {
            return (int)RolesEnum.user;
        }

    }
}
