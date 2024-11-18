using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public int client_car_id { get; set; }
        public int employee_id { get; set; }
        public DateTime date_in { get; set; }
        public DateTime? date_out { get; set; }
        public string description { get; set; } = string.Empty;
        public decimal? work_price { get; set; }
        public decimal? employee_income { get; set; }

        [ForeignKey("client_car_id")]
        public ClientCar clientCar { get; set; }
        [ForeignKey("employee_id")]
        public Employee employee { get; set; }
    }
}
