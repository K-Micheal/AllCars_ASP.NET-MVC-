using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllCars.Models
{
    public class SearchResult
    {
        public List<string> ids { get; set; }
        public int count { get; set; }
        public int last_id { get; set; }
    }
}
