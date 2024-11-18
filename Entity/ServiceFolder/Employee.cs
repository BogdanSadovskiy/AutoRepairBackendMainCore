namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Employee
    {
        public int id { get; set; }
        public string? employee_photo_file_path { get; set; }
        public int service_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public bool currently_working { get; set; }

        public Service service { get; set; }
    }
}
