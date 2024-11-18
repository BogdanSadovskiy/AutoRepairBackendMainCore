using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Model
    {
        [Key]
        public int id { get; set; }
        public string model_name { get; set; } = string.Empty;

      
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
