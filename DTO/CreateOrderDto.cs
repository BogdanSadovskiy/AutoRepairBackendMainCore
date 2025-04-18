using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.DTO
{
    public class CreateOrderDto
    {
        public ClientDto Client { get; set; }
        public ClientCarDto ClientCar { get; set; }
        public string? Description { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateIn { get; set; }
    }
}
