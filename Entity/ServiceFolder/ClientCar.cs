using AutoRepairMainCore.Entity.CarsGeneralFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class ClientCar
    {
        [Key]
        public int id { get; set; }
        public int car_id { get; set; }
        public string vin_code { get; set; } = string.Empty;
        [ForeignKey("car_id")]
        public Car car { get; set; }
 
    }
}
