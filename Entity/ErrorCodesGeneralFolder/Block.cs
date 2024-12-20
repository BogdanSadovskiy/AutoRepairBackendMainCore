using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ErrorCodesGeneralFolder
{
    [Table("blocks")]
    public class Block
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("block_name")]
        public string BlockName { get; set; } = string.Empty;

        public ICollection<ErrorCode> ErrorCodes { get; set; } = new List<ErrorCode>();
    }
}
