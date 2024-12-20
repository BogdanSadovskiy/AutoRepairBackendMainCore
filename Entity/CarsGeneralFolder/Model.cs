using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    [Table("models")]
    public class Model
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("model_name")]
        public string ModelName { get; set; } = string.Empty;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
