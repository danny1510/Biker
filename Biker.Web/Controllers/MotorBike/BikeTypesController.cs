using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;

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

        // GET: BikeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BikeTypes.ToListAsync());
        }

        // GET: BikeTypes/Details/5
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

        // GET: BikeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BikeTypeEntity bikeTypeEntity)
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

        // POST: BikeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BikeTypeEntity bikeTypeEntity)
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

        // GET: BikeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: BikeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bikeTypeEntity = await _context.BikeTypes.FindAsync(id);
            _context.BikeTypes.Remove(bikeTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikeTypeEntityExists(int id)
        {
            return _context.BikeTypes.Any(e => e.Id == id);
        }
    }
}
