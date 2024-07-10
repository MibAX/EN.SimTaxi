﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SimTaxi.Mvc.Data;
using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Models.Cars;

namespace EN.SimTaxi.Mvc.Controllers
{
    public class CarsController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var cars = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .ToListAsync();
            

            List<CarViewModel> listCarViewModel = new List<CarViewModel>();

            foreach (var car in cars) // 2 cars => two loops
            {
                CarViewModel carViewModel = new CarViewModel();
                carViewModel.Id = car.Id;
                carViewModel.Model = car.Model;
                carViewModel.Color = car.Color;
                carViewModel.Year = car.Year;
                carViewModel.PlateNumber = car.PlateNumber;
                carViewModel.PowerType = car.PowerType;
                carViewModel.CarType = car.CarType;

                if(car.Driver != null)
                {
                    carViewModel.DriverFullName = $"{car.Driver.FirstName} {car.Driver.LastName}";
                }
 
                listCarViewModel.Add(carViewModel);
            }

            return View(listCarViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Where(car => car.Id == id)
                                .SingleOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(car);
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

        #endregion

        #region Private Methods

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        } 

        #endregion
    }
}
