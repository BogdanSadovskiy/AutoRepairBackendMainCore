using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    [Table("order_file")]
    public class OrderFile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("file_path")]
        public string? FilePath { get; set; }

        [ForeignKey("OrderId")]
        public Order order { get; set; }
    }
}
