using AutoRepairMainCore.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AutoRepairMainCore.Entity.ServiceFolder
{
   
    public class MyService
    {
        public int id { get; set; }
        public string? service_icon_file_path { get; set; }
        public string service_name { get; set; } = string.Empty;
        public string service_password { get; set; } = string.Empty;
        public int role_id { get; set; }

        [ForeignKey("role_id")]
        public Role role { get; set; }
        [NotMapped]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
     


    }
}
