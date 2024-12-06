using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Engine
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("engine_description")]
        public string EngineDescription { get; set; } = string.Empty;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
