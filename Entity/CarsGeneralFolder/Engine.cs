﻿using System.ComponentModel.DataAnnotations;

namespace AutoRepairMainCore.Entity.CarsGeneralFolder
{
    public class Engine
    {
        [Key]
        public int id { get; set; }
        public string engine_description { get; set; } = string.Empty;

        
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
