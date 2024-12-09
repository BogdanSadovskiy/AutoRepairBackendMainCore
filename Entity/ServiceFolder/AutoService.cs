using AutoRepairMainCore.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class AutoService
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("service_icon_file_path")]
        public string? serviceIconFilePath { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("role_id")]
        public Role Role { get; set; }

        [NotMapped]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
