namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Order
    {
        public int id { get; set; }
        public int client_car_id { get; set; }
        public int employee_id { get; set; }
        public DateTime date_in { get; set; }
        public DateTime? date_out { get; set; }
        public string description { get; set; } = string.Empty;
        public decimal? work_price { get; set; }
        public decimal? employee_income { get; set; }

        
        public ClientCar clientCar { get; set; }
        public Employee employee { get; set; }
    }
}
