using EN.SimTaxi.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Drivers
{
    public class CreateUpdateDriverViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")] 
        public string FirstName { get; set; } // Sameer

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Cars")]
        public List<int> CarIds { get; set; } = []; // [4, 6, 8]

        //===========================================================

        public MultiSelectList CarsLookup { get; set; }
    }
}
