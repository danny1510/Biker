using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Helpers;
using Biker.Web.Models.Spare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.Spare
{
    public class SpareBrandsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public SpareBrandsController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper )
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(await _context.SpareBrands.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spareBrandEntity = await _context.SpareBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spareBrandEntity == null)
            {
                return NotFound();
            }

            return View(spareBrandEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSpareBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sparebrand = await _context.SpareBrands.FirstOrDefaultAsync
                (sb=> sb.Name.ToUpper() == model.Name.ToUpper() );

            if (sparebrand != null)
            {
                var error = "The Spare Brand can't be Added because it already exists.";
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "SpareBrands", error }));
            }

            var path = string.Empty;

            if (model.ImageFile != null)
            {
                path = await _imageHelper.UploadImageAsync(model.ImageFile, "SpareBrands");
            }

            sparebrand = _converterHelper.ToSpareBrandEntity(model, path);

           
                await _context.SpareBrands.AddAsync(sparebrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.SpareBrands.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var spareBrandviewmodel = new AddSpareBrandViewModel 
            { 
            Id = model.Id,
            BrandCategories = model.BrandCategories,
            ImageUrl = model.ImageUrl,
            Name = model.Name
            };

            return View(spareBrandviewmodel);
        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSpareBrandViewModel model)
        {
              if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "SpareBrands");
                    //TODO: investigar borrar imagen anterior
                    _imageHelper.DeleteImageAsync(model.ImageUrl);
                }

                var sparebrand = _converterHelper.ToSpareBrandEntity(model, path);


                try
                {
                    _context.SpareBrands.Update(sparebrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpareBrandEntityExists(model.Id))
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
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!SpareBrandEntityExists(id.Value))
            {
                return NotFound();
            }

            var spareBrandEntity = await _context.SpareBrands
                .FirstOrDefaultAsync(m => m.Id == id);

            if (spareBrandEntity == null)
            {
                return NotFound();
            }

            _context.SpareBrands.Remove(spareBrandEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpareBrandEntityExists(int id)
        {
            return _context.SpareBrands.Any(e => e.Id == id);
        }
    }
}
