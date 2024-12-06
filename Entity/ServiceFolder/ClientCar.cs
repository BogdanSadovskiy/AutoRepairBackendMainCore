using AutoRepairMainCore.Entity.CarsGeneralFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class ClientCar
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("car_id")]
        public int CarId { get; set; }
        [Column("vin_code")]
        public string VinCode { get; set; } = string.Empty;

        [ForeignKey("car_id")]
        public Car Car { get; set; }
    }
}
