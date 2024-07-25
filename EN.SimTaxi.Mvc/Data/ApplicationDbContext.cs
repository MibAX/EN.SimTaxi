using EN.SimTaxi.Mvc.Entities.Bookings;
using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Entities.Drivers;
using EN.SimTaxi.Mvc.Entities.Passengers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EN.SimTaxi.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
