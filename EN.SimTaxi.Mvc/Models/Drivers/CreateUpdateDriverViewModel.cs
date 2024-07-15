using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Drivers
{
    public class CreateUpdateDriverViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        //public List<Car> Cars { get; set; } = [];
    }
}
