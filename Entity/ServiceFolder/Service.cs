namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Service
    {
        public int id { get; set; }
        public string? service_icon_file_path { get; set; }
        public string service_name { get; set; } = string.Empty;
        public string service_password { get; set; } = string.Empty;

        
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
