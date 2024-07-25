using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SimTaxi.Mvc.Entities.Passengers
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }

        //============= Not Mapped ===============

        [NotMapped]
        public string FullName
        {
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
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }

    }
}
