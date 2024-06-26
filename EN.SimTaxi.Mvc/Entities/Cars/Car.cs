using EN.SimTaxi.Mvc.Enums;

namespace EN.SimTaxi.Mvc.Entities.Cars
{
    public class Car
    {
        public int Id { get; set; } // 1
        public string Model { get; set; } // Nissan GTR R35 Nismo
        public string Color { get; set; } // White with red lines
        public DateTime Year { get; set; } // 2024
        public string PlateNumber { get; set; } // 20 - 8521
        public PowerType PowerType { get; set; } // PowerType.Hybird
        public CarType CarType { get; set; } // CarType.Sedan

    }
}
