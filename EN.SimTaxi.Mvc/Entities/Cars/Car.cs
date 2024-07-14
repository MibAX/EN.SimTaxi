using EN.SimTaxi.Mvc.Entities.Drivers;
using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SimTaxi.Mvc.Entities.Cars
{
    public class Car
    {
        public int Id { get; set; } // 1
        public string Model { get; set; } // Nissan GTR R35 Nismo
        public string Color { get; set; } // White with red lines
        public DateTime ProductionDate { get; set; } // 2024-1-1:12:30:12:1234
        public string PlateNumber { get; set; } // 20 - 8521
        public PowerType PowerType { get; set; } // PowerType.Hybird
        public CarType CarType { get; set; } // CarType.Sedan


        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }

        [NotMapped]
        public int Year
        {
            get {
                return ProductionDate.Year; // 2024-1-1:12:30:12:1234 => 2024
            }
        }
    }
}
