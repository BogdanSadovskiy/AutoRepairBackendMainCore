using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;

namespace AutoRepairMainCore.DTO
{
    public class CreatingECUDto
    {
        public List<Block> ECUs { get; set; }
        public string Logger { get; set; }
    }
}
