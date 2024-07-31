using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EN.SimTaxi.Mvc.Data;
using EN.SimTaxi.Mvc.Entities.Bookings;
using AutoMapper;
using EN.SimTaxi.Mvc.Models.Bookings;

namespace EN.SimTaxi.Mvc.Controllers
{
    public class BookingsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookingsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _context
                                    .Bookings
                                    .Include(booking => booking.Car)
                                    .Include(booking => booking.Driver)
                                    .ToListAsync();

            var bookingVMs = _mapper.Map<List<Booking>, List<BookingViewModel>>(bookings);


            return View(bookingVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context
                                    .Bookings
                                    .Include(booking => booking.Car)
                                    .Include(booking => booking.Driver)
                                    .Include(booking => booking.Passengers)
                                    .Where(booking => booking.Id == id)
                                    .SingleOrDefaultAsync();

            if (booking == null)
            {
                return NotFound(); // 404
            }

            var bookingDetailsVM = _mapper.Map<Booking, BookingDetailsViewModel>(booking);

            return View(bookingDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateBookingVM = new CreateUpdateBookingViewModel();

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateBookingViewModel createUpdateBookingVM)
        {
            if (ModelState.IsValid)
            {
                var booking = _mapper.Map<CreateUpdateBookingViewModel, Booking>(createUpdateBookingVM);

                booking.Price = GetBookingPrice(booking);

                await UpdateBookingPassengers(booking, createUpdateBookingVM.PassengerIds);

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) // 123456
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context
                                    .Bookings
                                    .Include(booking => booking.Passengers)
                                    .Where(booking => booking.Id == id)
                                    .SingleOrDefaultAsync();

            if (booking == null)
            {
                return NotFound(); // 404
            }

            var createUpdateBookingVM = _mapper.Map<Booking, CreateUpdateBookingViewModel>(booking);

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateBookingViewModel createUpdateBookingVM)
        {
            if (id != createUpdateBookingVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO Load the Booking including the Passengers from the DB
                var booking = await _context
                                        .Bookings
                                        .Include(booking => booking.Passengers)
                                        .Where(booking => booking.Id == id)
                                        .SingleOrDefaultAsync();

                if(booking == null)
                {
                    return NotFound(); // 404
                }


                // Patch (copy) the values from CreateUpdateBookingViewModel => Booking 
                _mapper.Map<CreateUpdateBookingViewModel, Booking>(createUpdateBookingVM, booking);

                // Update the booking passengers
                await UpdateBookingPassengers(booking, createUpdateBookingVM.PassengerIds);

                // Update the price
                booking.Price = GetBookingPrice(booking);


                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        private decimal GetBookingPrice(Booking booking)
        {
            // TO DO this is a simulation using the random class 
            // in a real project there will be code for a pricing engine that 
            // depeneds on GoogleMaps integration

            Random random = new Random();
            decimal minValue = 10.0m;
            decimal maxValue = 100.0m;
            decimal randomPrice = (decimal)(random.NextDouble() * (double)(maxValue - minValue) + (double)minValue);

            return Math.Round(randomPrice, 2); // A random number between 10 and 100
        }

        private async Task UpdateBookingPassengers(Booking booking, List<int> passengerIds)
        {
            // TO DO: Clear Passengers from Booking
            booking.Passengers.Clear();

            // TO DO: Get the Passengers from the database using passengerIds
            var passengers = await _context
                                        .Passengers
                                        .Where(passenger => passengerIds.Contains(passenger.Id)) // [5 ,6] => [ {5, Asad}, {6, Natasha }]
                                        .ToListAsync();

            // TO DO: Add the loaded Passenger to the Booking
            booking.Passengers.AddRange(passengers);
        }

        #endregion
    }
}
