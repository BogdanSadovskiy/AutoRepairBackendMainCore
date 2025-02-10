namespace AutoRepairMainCore.DTO
{
    public class CreateOrderDto
    {
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public int? ClientCarId { get; set; }
        public int? CarId { get; set; }
        public string? VinCode { get; set; }
        public string? Description { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateIn { get; set; }
    }
}
