using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("client_car_id")]
        public int ClientCarId { get; set; }
        [Column ("employee_id")]
        public int EmployeeId { get; set; }
        [Column("date_in")]
        public DateTime DateIn { get; set; }
        [Column("date_out")]
        public DateTime? DateOut { get; set; }
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("work_price")]
        public decimal? WorkPrice { get; set; }
        [Column("employee_income")]
        public decimal? EmployeeIncome { get; set; }

        [ForeignKey("ClientCarId")]
        public ClientCar ClientCar { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
