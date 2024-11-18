using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class ErrorCodeOrder
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int error_code_id { get; set; }
        public DateTime date { get; set; }

        
        public Order order { get; set; }
        public ErrorCode errorCode { get; set; }
    }
}
