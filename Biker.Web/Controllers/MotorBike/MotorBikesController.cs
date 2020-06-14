using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Helpers;
using Biker.Web.Models.MotorBike;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.MotorBike
{
    public class MotorBikesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public MotorBikesController(
            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public IActionResult Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(_context.MotorBikes
                .Include(m => m.MotorBikeSpares)
                .Include(m => m.TypeMaker)
                .ThenInclude(tm => tm.BikeMaker)
                .Include(m => m.TypeMaker)
                .ThenInclude(tm => tm.BikeType)
                .OrderBy(m => m.Name)
                .OrderBy(m => m.TypeMaker.BikeMaker.Name)
                );
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string error)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            //TODO: ordenar por años since descendente
            var motorBikeEntity = await _context.MotorBikes
                .Include(m => m.MotorBikeSpares)
                .OrderByDescending(m => m.MotorBikeSpares.OrderByDescending(mts => mts.YearSince))
                .Include(m => m.TypeMaker)
                .ThenInclude(tm => tm.BikeType)
                .Include(m => m.TypeMaker)
                .ThenInclude(tm => tm.BikeMaker)              
                .FirstOrDefaultAsync(m => m.Id == id)
                ;

            if (motorBikeEntity == null)
            {
                return NotFound();
            }

            return View(motorBikeEntity);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var model = new AddMotorBikeViewModel
            {
                Makers = _combosHelper.GetComboMakers(),
                Types = _combosHelper.GetComboTypes(),
                Cylinder = 125,
                FrontTire = 17,
                HeightTireF = 100,
                HeightTireR = 100,
                RearTire = 17,
                WidthTireF = 100,
                WidthTireR = 100,
                Millimeters = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMotorBikeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var motorbike = await _converterHelper.ToMotorbikeEntityasync(model);

                await _context.MotorBikes.AddAsync(motorbike);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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

            var motorBike = await _context.MotorBikes
                 .Include(m => m.TypeMaker)
                 .ThenInclude(m => m.BikeMaker)
                 .Include(m => m.TypeMaker)
                 .ThenInclude(m => m.BikeType)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (motorBike == null)
            {
                return NotFound();
            }

            var MotorBikeSpareViewModel = _converterHelper.ToMotorBikeViewModel(motorBike);

            return View(MotorBikeSpareViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddMotorBikeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!MotorBikeEntityExists(model.Id))
            {
                return NotFound();
            }

            var motorbike = await _converterHelper.ToMotorbikeEntityasync(model);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.MotorBikes.Update(motorbike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException err)
                {
                    ModelState.AddModelError(string.Empty, err.Message);

                }
                return RedirectToAction(nameof(Index));
            }

            model.Makers = _combosHelper.GetComboMakers();
            model.Types = _combosHelper.GetComboTypes();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBike = await _context.MotorBikes
                 .Include(m => m.TypeMaker)
                 .ThenInclude(tm => tm.BikeType)
                 .Include(m => m.MotorBikeSpares)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (motorBike == null)
            {
                return NotFound();
            }

            if (motorBike.MotorBikeSpares.Count > 0)
            {
                var error = "The Motorbike can't be deleted because it has related records. ";
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "MotorBikes", error }));
            }

            try
            {
                _context.MotorBikes.Remove(motorBike);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception err)
            {

                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "MotorBikes", err.Message }));
            }


            return RedirectToAction($"{nameof(Index)}");
        }

        private bool MotorBikeEntityExists(int id)
        {
            return _context.MotorBikes.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> AddBikeS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBike = await _context.MotorBikes.FindAsync(id.Value);

            if (motorBike == null)
            {
                return NotFound();
            }

            var model = new AddMotorBikeSpareViewModel
            {
                MotorbikeId = motorBike.Id,
                YearSince = 2020,
                YearUntil = 2020,
                Id = 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBikeS(AddMotorBikeSpareViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "MotorBikesSpare");
                }

                var motorbikeSpare = await _converterHelper.ToMotorbikeSpareEntityasync(model, path, true);

                await _context.MotorBikeSpares.AddAsync(motorbikeSpare);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Details/{model.MotorbikeId}");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditBikeS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeSpare = await _context.MotorBikeSpares
                 .Include(m => m.MotorBike)
                 .FirstOrDefaultAsync(m => m.Id == id);

            var MotorBikeSpareViewModel = _converterHelper.ToMotorBikeSpareViewModel(motorBikeSpare);

            return View(MotorBikeSpareViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBikeS(AddMotorBikeSpareViewModel model)
        {

            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "MotorBikesSpare");
                }

                var motorbikeSpare = await _converterHelper.ToMotorbikeSpareEntityasync(model, path, false);
                _context.MotorBikeSpares.Update(motorbikeSpare);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.MotorbikeId}");
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteBikeS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorBikeSpare = await _context.MotorBikeSpares
                 .Include(m => m.MotorBike)
                 .Include(m => m.BikerMotors)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (motorBikeSpare == null)
            {
                return NotFound();
            }

            if (motorBikeSpare.BikerMotors.Count > 0)
            {
                var error = "The Motorbike Spare can't be deleted because it has related records.";
                return RedirectToAction($"{nameof(Details)}/{motorBikeSpare.MotorBike.Id}", new RouteValueDictionary(new
                {
                    Controller = "MotorBikes",
                    error
                }));

            }

            try
            {
                _context.MotorBikeSpares.Remove(motorBikeSpare);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception err)
            {
                return RedirectToAction($"{nameof(Details)}/{motorBikeSpare.MotorBike.Id}", new RouteValueDictionary(new { Controller = "MotorBikes", err.Message }));
            }

            return RedirectToAction($"{nameof(Details)}/{motorBikeSpare.MotorBike.Id}");

        }


        public async Task<JsonResult> GetTypesAsync(int makerId)
        {
            var maker = await _context.BikeMakers
                //.Include(tm=> tm.Maker)
                .Include(bm => bm.TypeMaker)
                .ThenInclude(tm => tm.BikeType)
                //.Where(tm => tm.Id == makerId)
                //.OrderBy(tm => tm.Type.Name)
                .FirstOrDefaultAsync(tm => tm.Id == makerId);
            //.OrderBy(tm => tm.Type.Name)
            //.ToListAsync();

            var typeMakers = new BikeMakerEntity
            {
                Id = maker.Id,
                Name = maker.Name,
                TypeMaker = maker.TypeMaker.Select(tm => new TypeMakerEntity
                {
                    Id = tm.Id,
                    BikeType = new BikeTypeEntity
                    {
                        Id = tm.BikeType.Id,
                        Name = tm.BikeType.Name,
                    }
                }).ToList()
            };


            return Json(typeMakers.TypeMaker);
        }

        public IActionResult IsMillimeters(bool millimeters, AddMotorBikeViewModel model)
        {
            var modelo = new AddMotorBikeViewModel
            {
                Makers = model.Makers,
                Types = model.Types,
                Millimeters = millimeters,
                Name = model.Name,
                Cylinder = model.Cylinder,
                FrontTire = model.FrontTire,
                HeightTireF = model.HeightTireF,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                WidthTireF = model.WidthTireF,
                WidthTireR = model.WidthTireR,

            };
            return View(modelo);

        }

    }
}
