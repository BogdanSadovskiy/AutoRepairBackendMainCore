namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Brand
    {
        public int id { get; set; }
        public string brand_name { get; set; } = string.Empty;

        
        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}
