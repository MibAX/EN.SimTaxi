using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Entities.Passengers;
using EN.SimTaxi.Mvc.Models.Passengers;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Bookings
{
    public class BookingDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "From Address")]
        public string FromAddress { get; set; }

        [Display(Name = "To Address")]
        public string ToAddress { get; set; }

        [Display(Name = "Booking Time")]
        public DateTime BookingTime { get; set; }

        [Display(Name = "Car")]
        public string CarInfo { get; set; }

        [Display(Name = "Driver")]
        public string DriverFullName { get; set; }

        public List<PassengerViewModel> Passengers { get; set; } = [];
    }
}
