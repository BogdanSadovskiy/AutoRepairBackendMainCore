using AutoRepairMainCore.Entity.ServiceFolder;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity
{
    public class Role
    {
        [Key]
        public int id {  get; set; }
        public string role_name { get; set; } = "user";
        public ICollection<MyService> Services { get; set; } = new List<MyService>();
    }
}
