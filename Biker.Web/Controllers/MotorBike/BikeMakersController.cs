using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.MotorBike
{
    [Authorize(Roles = "Admin")]
    public class BikeMakersController : Controller
    {
        private readonly DataContext _context;

        public BikeMakersController(DataContext context)
        {
            _context = context;
        }

        // GET: BikeMakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.BikeMakers.ToListAsync());
        }

        // GET: BikeMakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeMakerEntity = await _context.BikeMakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikeMakerEntity == null)
            {
                return NotFound();
            }

            return View(bikeMakerEntity);
        }

        // GET: BikeMakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikeMakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl")] BikeMakerEntity bikeMakerEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bikeMakerEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikeMakerEntity);
        }

        // GET: BikeMakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeMakerEntity = await _context.BikeMakers.FindAsync(id);
            if (bikeMakerEntity == null)
            {
                return NotFound();
            }
            return View(bikeMakerEntity);
        }

        // POST: BikeMakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl")] BikeMakerEntity bikeMakerEntity)
        {
            if (id != bikeMakerEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikeMakerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeMakerEntityExists(bikeMakerEntity.Id))
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
            return View(bikeMakerEntity);
        }

        // GET: BikeMakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeMakerEntity = await _context.BikeMakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikeMakerEntity == null)
            {
                return NotFound();
            }

            return View(bikeMakerEntity);
        }

        // POST: BikeMakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bikeMakerEntity = await _context.BikeMakers.FindAsync(id);
            _context.BikeMakers.Remove(bikeMakerEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikeMakerEntityExists(int id)
        {
            return _context.BikeMakers.Any(e => e.Id == id);
        }
    }
}
