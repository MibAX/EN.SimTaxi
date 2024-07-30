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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", booking.CarId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", booking.DriverId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromAddress,ToAddress,BookingTime,CarId,DriverId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", booking.CarId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", booking.DriverId);
            return View(booking);
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

            return randomPrice; // A random number between 10 and 100
        }

        #endregion
    }
}
