using AutoRepairMainCore.Entity.CarsGeneralFolder;

namespace AutoRepairMainCore.Entity.ServiceFolder
{
    public class ClientCar
    {
        public int id { get; set; }
        public int car_id { get; set; }
        public string vin_code { get; set; } = string.Empty;
        public Car car { get; set; }
 
    }
}
