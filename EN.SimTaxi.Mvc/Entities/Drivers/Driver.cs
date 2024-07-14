using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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


        [NotMapped]
        public string FullName { 
            get
            {
                return $"{FirstName} {LastName}";

            }
        }

        [NotMapped]
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year; // for example: 2024 - 2000 => return 24
            }
        }
    }
}
