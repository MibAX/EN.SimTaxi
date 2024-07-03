using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Enums;

namespace EN.SimTaxi.Mvc.Entities.Drivers
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public List<Car> Cars { get; set; } = [];
    }
}
