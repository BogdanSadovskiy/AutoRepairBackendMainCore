using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Car
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Column("model_id")]
        public int ModelId { get; set; }
        [Column ("engine_id")]
        public int EngineId { get; set; }

        [ForeignKey("brand_id")]
        public Brand Brand { get; set; }

        [ForeignKey("model_id")]
        public Model Model { get; set; }

        [ForeignKey("engine_id")]
        public Engine Engine { get; set; }
    }
}
