namespace AutoRepairMainCore.Entity.ErrorCodesGeneralFolder
{
    public class ErrorCode
    {
        public int id { get; set; }
        public string code { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int block_id { get; set; }

        public Block block { get; set; }
    }
}
