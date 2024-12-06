using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("employee_photo_file_path")]
        public string? EmployeePhotoFilePath { get; set; }
        [Column ("autoservice_id")]
        public int AutoServiceId { get; set; }
        [Column("employee_name")]
        public string EmployeeName { get; set; } = string.Empty;
        [Column ("currently_working")]
        public bool CurrentlyWorking { get; set; }

        [ForeignKey("autoservice_id")]
        public AutoService AutoService { get; set; }
    }
}
