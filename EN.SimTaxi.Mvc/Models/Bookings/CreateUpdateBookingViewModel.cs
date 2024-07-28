using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Bookings
{
    public class CreateUpdateBookingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "From Address")]
        public string FromAddress { get; set; }

        [Display(Name = "To Address")]
        public string ToAddress { get; set; }

        [Display(Name = "Booking Time")]
        public DateTime BookingTime { get; set; }

        [Display(Name = "Car")]
        public int? CarId { get; set; }

        [Display(Name = "Driver")]
        public int? DriverId { get; set; }

        [Display(Name = "Passengers")]
        public List<int> PassengerIds { get; set; } = [];

        //============= Those are for choosing from the Page and NOT to create/edit a Booking ===================

        [ValidateNever]
        public SelectList CarLookup { get; set; }
        
        [ValidateNever]
        public SelectList DriverLookup { get; set; }

        [ValidateNever]
        public MultiSelectList PassengerLookup { get; set; }

    }
}
