using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SimTaxi.Mvc.Data;
using EN.SimTaxi.Mvc.Entities.Passengers;
using AutoMapper;
using EN.SimTaxi.Mvc.Models.Passengers;

namespace EN.SimTaxi.Mvc.Controllers
{
    public class PassengersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PassengersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var passengers = await _context
                                        .Passengers
                                        .ToListAsync();

            var passengerVMs = _mapper.Map<List<Passenger>, List<PassengerViewModel>>(passengers);

            return View(passengerVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context
                                    .Passengers
                                    .Where(passenger => passenger.Id == id)
                                    .SingleOrDefaultAsync();

            if (passenger == null)
            {
                return NotFound();
            }

            var passengerVM = _mapper.Map<Passenger, PassengerDetailsViewModel>(passenger);

            return View(passengerVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdatePassengerViewModel createUpdatePassengerViewModel)
        {
            if (ModelState.IsValid)
            {
                var passenger = _mapper.Map<CreateUpdatePassengerViewModel, Passenger>(createUpdatePassengerViewModel);

                _context.Add(passenger);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdatePassengerViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context
                                    .Passengers // TO DO include Booking Entity
                                    .FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            var createUpdatePassengerVM = _mapper.Map<Passenger, CreateUpdatePassengerViewModel>(passenger);

            return View(createUpdatePassengerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdatePassengerViewModel createUpdatePassengerViewModel)
        {
            if (id != createUpdatePassengerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO get the passenger from the database
                var passenger = await _context
                                        .Passengers
                                        .FindAsync(id); // TO DO change to include bookings

                if (passenger == null)
                {
                    return NotFound();
                }

                // TO DO patch (copy) the createUpdatePassengerViewModel into passenger
                _mapper.Map(createUpdatePassengerViewModel, passenger);

                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(createUpdatePassengerViewModel.Id))
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

            return View(createUpdatePassengerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.Id == id);
        } 

        #endregion
    }
}
