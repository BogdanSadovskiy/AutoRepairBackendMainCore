namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Car
    {
        public int id { get; set; }
        public int mark_id { get; set; }
        public int model_id { get; set; }
        public int engine_id { get; set; }

       
        public Brand brand { get; set; }
        public Model model { get; set; }
        public Engine engine { get; set; }
    }
}
