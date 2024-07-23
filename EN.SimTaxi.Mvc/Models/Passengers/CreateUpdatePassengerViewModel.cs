using EN.SimTaxi.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EN.SimTaxi.Mvc.Models.Passengers
{
    public class CreateUpdatePassengerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }

        //=============================================

        [ValidateNever]
        public string FullName { get; set; }

    }
}
