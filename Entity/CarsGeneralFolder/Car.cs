using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    [Table("cars")]
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

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [ForeignKey("ModelId")]
        public Model Model { get; set; }

        [ForeignKey("EngineId")]
        public Engine Engine { get; set; }
    }
}
