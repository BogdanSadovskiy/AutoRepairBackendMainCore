using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ErrorCodesGeneralFolder
{
    [Table("error_codes")]
    public class ErrorCode
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("block_id")]
        public int BlockId { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
  
        [ForeignKey("BlockId")]
        public Block Block { get; set; }
    }
}
