using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Car
    {
        [Key]
        public int id { get; set; }
        
        
        public int brand_id { get; set; }
        public int model_id { get; set; }
        public int engine_id { get; set; }

        [ForeignKey("brand_id")]
        public Brand brand { get; set; }
        [ForeignKey("model_id")]
        public Model model { get; set; }
        [ForeignKey("engine")]
        public Engine engine { get; set; }
    }
}
