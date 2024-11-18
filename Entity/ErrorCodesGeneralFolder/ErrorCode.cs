using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ErrorCodesGeneralFolder
{
    public class ErrorCode
    {
        [Key]
        public int id { get; set; }
        public string code { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int block_id { get; set; }

        [ForeignKey("block_id")]
        public Block block { get; set; }
    }
}
