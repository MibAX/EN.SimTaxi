using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Entities.Drivers;
using EN.SimTaxi.Mvc.Entities.Passengers;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SimTaxi.Mvc.Entities.Bookings
{
    public class Booking
    {
        public int Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime BookingTime { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public bool IsPaid { get; set; } // true = paid #### false = not paid
        public DateTime? PaymentDate { get; set; }


        public int? CarId { get; set; }
        public Car Car { get; set; }

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        public List<Passenger> Passengers { get; set; } = [];
    }
}
