using Biker.Web.Data;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Helpers;
using Biker.Web.Models.MotorBike;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.MotorBike
{
    public class MotorBikesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public MotorBikesController(
            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.MotorBikes
                .Include(m => m.MotorBikeSpares)
                .Include(m => m.BikeMaker)
                .Include(m => m.BikeType)
                .OrderBy(m => m.BikeMaker.Name)
                .OrderBy(m => m.Name)
                );
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeEntity = await _context.MotorBikes
                .Include(m => m.MotorBikeSpares)
                .Include(m => m.BikeType)
                .Include(m => m.BikeMaker)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorBikeEntity == null)
            {
                return NotFound();
            }

            return View(motorBikeEntity);
        }

        public IActionResult Create()
        {
            var model = new AddMotorBikeViewModel
            {
                Makers = _combosHelper.GetComboMakers(),
                Types =  _combosHelper.GetComboTypes(),
                Millimeters = true
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMotorBikeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                //if (model.ImageFile != null)
                //{
                //    var guid = Guid.NewGuid().ToString();
                //    var file = $"{guid}.jpg";

                //    path = Path.Combine(
                //    Directory.GetCurrentDirectory(),
                //    "wwwroot\\images\\MotorBikesSpare",
                //    file);

                //    using (var stream = new FileStream(path, FileMode.Create))
                //    {
                //        await model.ImageFile.CopyToAsync(stream);
                //    }
                //    path = $"~/images/Pets/{file}";

                //}

                var motorbike = await _converterHelper.ToMotorbikeasync(model);

                await _context.MotorBikes.AddAsync(motorbike);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddbikeS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeEntity = await _context.MotorBikes
               .Include(m => m.MotorBikeSpares)
               .Include(m => m.BikeType)
               .Include(m => m.BikeMaker)
               .FirstOrDefaultAsync(m => m.Id == id);


            var model = new AddMotorBikeSpareViewModel
            {
                MotorbikeId = motorBikeEntity.Id,
                MotorBike   = motorBikeEntity,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBikeS(AddMotorBikeSpareViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                try
                {
                    var motorbike = await _context.MotorBikes.FindAsync(model.MotorbikeId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }

                if (model.ImageFile != null)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\MotorBikesSpare",
                    file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    path = $"~/images/MotorBikesSpare/{file}";

                }

                var motorbikeSpare = await _converterHelper.ToMotorbikeSpareasync(model, path);

                await _context.MotorBikeSpares.AddAsync(motorbikeSpare);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Details/{model.MotorbikeId}");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeEntity = await _context.MotorBikes.FindAsync(id);
            if (motorBikeEntity == null)
            {
                return NotFound();
            }
            return View(motorBikeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cylinder,YearSince")] MotorBikeEntity motorBikeEntity)
        {
            if (id != motorBikeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorBikeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorBikeEntityExists(motorBikeEntity.Id))
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
            return View(motorBikeEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeEntity = await _context.MotorBikes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorBikeEntity == null)
            {
                return NotFound();
            }

            return View(motorBikeEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorBikeEntity = await _context.MotorBikes.FindAsync(id);
            _context.MotorBikes.Remove(motorBikeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorBikeEntityExists(int id)
        {
            return _context.MotorBikes.Any(e => e.Id == id);
        }
    }
}
