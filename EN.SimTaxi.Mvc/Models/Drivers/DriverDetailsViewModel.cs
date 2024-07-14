using EN.SimTaxi.Mvc.Enums;
using EN.SimTaxi.Mvc.Models.Cars;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Drivers
{
    public class DriverDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }
}
