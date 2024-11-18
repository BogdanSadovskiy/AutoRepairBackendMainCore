using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string? employee_photo_file_path { get; set; }
        public int service_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public bool currently_working { get; set; }
        [ForeignKey("service_id")]
        public Service service { get; set; }
    }
}
