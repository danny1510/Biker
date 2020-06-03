using Biker.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVet.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers
{
    public class BikersController : Controller
    {
        private readonly DataContext _context;

        public BikersController(DataContext context)
        {
            _context = context;
        }

        // GET: BikerEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bikers.ToListAsync());
        }

        // GET: BikerEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikerEntity == null)
            {
                return NotFound();
            }

            return View(bikerEntity);
        }

        // GET: BikerEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikerEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BikerEntity bikerEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bikerEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikerEntity);
        }

        // GET: BikerEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers.FindAsync(id);
            if (bikerEntity == null)
            {
                return NotFound();
            }
            return View(bikerEntity);
        }

        // POST: BikerEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] BikerEntity bikerEntity)
        {
            if (id != bikerEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikerEntityExists(bikerEntity.Id))
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
            return View(bikerEntity);
        }

        // GET: BikerEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bikerEntity == null)
            {
                return NotFound();
            }

            return View(bikerEntity);
        }

        // POST: BikerEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bikerEntity = await _context.Bikers.FindAsync(id);
            _context.Bikers.Remove(bikerEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikerEntityExists(int id)
        {
            return _context.Bikers.Any(e => e.Id == id);
        }
    }
}
