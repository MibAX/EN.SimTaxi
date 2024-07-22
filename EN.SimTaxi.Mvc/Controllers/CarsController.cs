using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SimTaxi.Mvc.Data;
using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Models.Cars;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EN.SimTaxi.Mvc.Controllers
{
    public class CarsController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .ToListAsync();

            List<CarViewModel> listCarViewModel = _mapper.Map<List<Car>, List<CarViewModel>>(cars);

            return View(listCarViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .Where(car => car.Id == id)
                                .SingleOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            var carDetailsViewModel = _mapper.Map<Car, CarDetailsViewModel>(car);

            return View(carDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateCarViewModel = new CreateUpdateCarViewModel();

            createUpdateCarViewModel.DriversLookup = new SelectList(_context.Drivers, "Id", "FullName");

            return View(createUpdateCarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCarViewModel createUpdateCarViewModel)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<CreateUpdateCarViewModel, Car>(createUpdateCarViewModel);

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            createUpdateCarViewModel.DriversLookup = new SelectList(_context.Drivers, "Id", "FullName");
            return View(createUpdateCarViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .Where(car => car.Id == id)
                                .SingleOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }


            var createUpdateCarViewModel = _mapper.Map<Car, CreateUpdateCarViewModel>(car);

            createUpdateCarViewModel.DriversLookup = new SelectList(_context.Drivers, "Id", "FullName");

            return View(createUpdateCarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCarViewModel createUpdateCarViewModel)
        {
            if (id != createUpdateCarViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var car = await _context
                                    .Cars
                                    .Include(car => car.Driver)
                                    .Where(car => car.Id == id)
                                    .SingleOrDefaultAsync();

                if (car == null)
                {
                    return NotFound();
                }


                _mapper.Map<CreateUpdateCarViewModel, Car>(createUpdateCarViewModel, car); // Copy (Patch) the Car with data from CreateUpdateCarViewModel


                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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

            createUpdateCarViewModel.DriversLookup = new SelectList(_context.Drivers, "Id", "FullName");
            return View(createUpdateCarViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context
                                .Cars
                                .FindAsync(id);

            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnassignCar(int id)
        {
            var car = await _context
                                .Cars
                                .FindAsync(id);

            if (car != null) 
            {
                car.DriverId = null;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        } 

        #endregion
    }
}
