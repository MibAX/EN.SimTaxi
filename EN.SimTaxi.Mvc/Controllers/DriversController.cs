using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SimTaxi.Mvc.Data;
using EN.SimTaxi.Mvc.Entities.Drivers;
using AutoMapper;
using EN.SimTaxi.Mvc.Models.Drivers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EN.SimTaxi.Mvc.Controllers
{
    public class DriversController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DriversController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drivers = await _context
                                    .Drivers
                                    .ToListAsync();

            var driverViewModels = _mapper.Map<List<Driver>, List<DriverViewModel>>(drivers);


            return View(driverViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context
                                    .Drivers
                                    .Include(driver => driver.Cars)
                                    .Where(driver => driver.Id == id)
                                    .SingleOrDefaultAsync();

            if (driver == null)
            {
                return NotFound(); // 404
            }

            var driverViewModel = _mapper.Map<Driver, DriverDetailsViewModel>(driver);

            return View(driverViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateDriverViewModel = new CreateUpdateDriverViewModel();

            createUpdateDriverViewModel.CarsLookup = new MultiSelectList(_context.Cars, "Id", "Info");

            return View(createUpdateDriverViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateDriverViewModel createUpdateDriverViewModel)
        {
            if (ModelState.IsValid)
            {
                var driver = _mapper.Map<CreateUpdateDriverViewModel, Driver>(createUpdateDriverViewModel);

                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateDriverViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context
                                    .Drivers
                                    .FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            var createUpdateDriverViewModel = _mapper.Map<Driver, CreateUpdateDriverViewModel>(driver);

            return View(createUpdateDriverViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateDriverViewModel createUpdateDriverViewModel)
        {
            if (id != createUpdateDriverViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var driver = await _context.Drivers.FindAsync(id);

                if (driver == null) 
                {
                    return NotFound();
                }

                _mapper.Map(createUpdateDriverViewModel, driver);

                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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

            return View(createUpdateDriverViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context
                                    .Drivers
                                    .FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(driver => driver.Id == id);
        } 

        #endregion
    }
}
