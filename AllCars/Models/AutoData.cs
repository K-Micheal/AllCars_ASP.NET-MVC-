using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllCars.Models
{
    public class AutoData
    {
        public bool active { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public bool onModeration { get; set; }
        public int year { get; set; }
        public int autoId { get; set; }
        public int bodyId { get; set; }
        public int statusId { get; set; }
        public bool withVideo { get; set; }
        public string race { get; set; }
        public int raceInt { get; set; }
        public int fuelId { get; set; }
        public string fuelName { get; set; }
        public string fuelNameEng { get; set; }
        public int gearBoxId { get; set; }
        public string gearboxName { get; set; }
        public int driveId { get; set; }
        public string driveName { get; set; }
        public bool isSold { get; set; }
        public string mainCurrency { get; set; }
        public bool fromArchive { get; set; }
        public int categoryId { get; set; }
        public string categoryNameEng { get; set; }
        public string subCategoryNameEng { get; set; }
        public int custom { get; set; }
        public bool withVideoMessages { get; set; }
    }
}
