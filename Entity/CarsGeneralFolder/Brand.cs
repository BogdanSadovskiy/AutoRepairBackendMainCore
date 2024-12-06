using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Brand
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("brand_name")]
        public string BrandName { get; set; } = string.Empty;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
