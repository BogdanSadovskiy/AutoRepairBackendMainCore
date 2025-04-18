namespace AutoRepairMainCore.DTO
{
    public class ClientCarDto
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        public string VinCode { get; set; } = string.Empty;
    }
}
