using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Passengers
{
    public class PassengerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
    }
}
