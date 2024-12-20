using AutoRepairMainCore.Entity.CarsGeneralFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    [Table("client_car")]
    public class ClientCar
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("car_id")]
        public int CarId { get; set; }
        [Column("vin_code")]
        public string VinCode { get; set; } = string.Empty;

        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
