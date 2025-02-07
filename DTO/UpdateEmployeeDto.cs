namespace AutoRepairMainCore.DTO
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsCurrentlyWorks { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
