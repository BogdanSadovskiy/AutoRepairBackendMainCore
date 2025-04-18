namespace AutoRepairMainCore.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Engine { get; set; } = string.Empty;
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string? ImageUrl { get; set; }
    }

}
