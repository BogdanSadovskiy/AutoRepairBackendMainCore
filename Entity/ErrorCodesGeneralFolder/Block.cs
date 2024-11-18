using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity.ErrorCodesGeneralFolder
{
    public class Block
    {
        [Key]
        public int id { get; set; }
        public string block_name { get; set; } = string.Empty;

       
        public ICollection<ErrorCode> ErrorCodes { get; set; } = new List<ErrorCode>();
    }
}
