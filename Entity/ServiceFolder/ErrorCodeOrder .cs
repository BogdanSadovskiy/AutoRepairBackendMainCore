using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    [Table("error_code_orders")]
    public class ErrorCodeOrder
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column ("error_code_id")]
        public int ErrorCodeId { get; set; }
        [Column("data")]
        public DateTime Date { get; set; }

        [ForeignKey("OrderId")]
        public Order order { get; set; }

        [ForeignKey("ErrorCodeId")]
        public ErrorCode errorCode { get; set; }
    }
}
