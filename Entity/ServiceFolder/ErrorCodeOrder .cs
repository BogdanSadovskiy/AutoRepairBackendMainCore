using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class ErrorCodeOrder
    {
        [Key]
        public int id { get; set; }
        public int order_id { get; set; }
        public int error_code_id { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("order_id")]
        public Order order { get; set; }
        [ForeignKey("error_code_id")]
        public ErrorCode errorCode { get; set; }
    }
}
