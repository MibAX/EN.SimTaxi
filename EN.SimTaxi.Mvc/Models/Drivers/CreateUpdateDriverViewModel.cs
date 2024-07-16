using EN.SimTaxi.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public List<int> CarIds { get; set; } = []; // [4, 8]



        //============= Those are for choosing from the Page and NOT to create/edit a driver ===================

        [ValidateNever]
        public MultiSelectList CarsLookup { get; set; }
    }
}
