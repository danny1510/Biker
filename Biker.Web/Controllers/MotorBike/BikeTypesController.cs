using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.MotorBike
{
    [Authorize(Roles = "Admin")]
    public class BikeTypesController : Controller
    {
        private readonly DataContext _context;

        public BikeTypesController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }


            return View(_context.BikeTypes
                .Include(bm => bm.TypeCategories)
                .ThenInclude(tm => tm.SpareCategory)
                .OrderBy(m => m.Name));

            //return View(await _context.BikeTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeTypeEntity = await _context.BikeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikeTypeEntity == null)
            {
                return NotFound();
            }

            return View(bikeTypeEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( BikeTypeEntity bikeTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bikeTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikeTypeEntity);
        }

        // GET: BikeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeTypeEntity = await _context.BikeTypes.FindAsync(id);
            if (bikeTypeEntity == null)
            {
                return NotFound();
            }
            return View(bikeTypeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BikeTypeEntity bikeTypeEntity)
        {
            if (id != bikeTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikeTypeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeTypeEntityExists(bikeTypeEntity.Id))
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
            return View(bikeTypeEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var bikeTypeEntity = _context.BikeTypes
                .Include(t => t.TypeMaker)
                .ThenInclude (tm=> tm.Motorbikes)
                .FirstOrDefault(t=> t.Id == id)
                ;
            
            if (bikeTypeEntity == null)
            {
                return NotFound();
            }

            if (bikeTypeEntity.TypeMaker.Count > 0)
            {
                var error = "The Motorbike type can't be deleted because it has related records.";
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeTypes", error }));
            }

            try
            {
                _context.BikeTypes.Remove(bikeTypeEntity);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception err)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeTypes", err.Message }));
            }

            return RedirectToAction(nameof(Index));

        }


        private bool BikeTypeEntityExists(int id)
        {
            return _context.BikeTypes.Any(e => e.Id == id);
        }
    }
}
