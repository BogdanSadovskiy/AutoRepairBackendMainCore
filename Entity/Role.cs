using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity
{
    public class Role
    {
        [Key]
        public int id {  get; set; }
        public string role_name { get; set; }
        public ICollection<Role> roles { get; set; }
    }
}
