using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllCars.Models
{
    public class Search
    {
        public string Mark { get; set; }
        public string Category { get; set; }
        public string Model { get; set; }
        public string Region { get; set; }
        public string PriceFrom { get; set; }
        public string PriceTo { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string GearBox { get; set; }
        public string VoluemEngineFrom { get; set; }
        public string VoluemEngineTo { get; set; }

    }
}
