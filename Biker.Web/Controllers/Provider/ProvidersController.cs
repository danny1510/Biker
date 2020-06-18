using Biker.Web.Data;
using Biker.Web.Data.Entities.Provider;
using Biker.Web.Helpers;
using Biker.Web.Models.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.Provider
{
    public class ProvidersController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ProvidersController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
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

            return View(await _context.Providers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerEntity = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (providerEntity == null)
            {
                return NotFound();
            }

            return View(providerEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProviderViewModel model)
        {
            if (ModelState.IsValid)
            {

                var provider = await _context.Providers.FirstOrDefaultAsync
                (sc => sc.Name.ToUpper() == model.Name.ToUpper());

                if (provider != null)
                {
                    var error = "The Provider can't be Added because it already exists.";
                    return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Provider", error }));
                }
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Providers");
                }

                provider = _converterHelper.ToProviderEntity(model, path);

                _context.Providers.Add(provider);
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

            var model = await _context.Providers.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var providerviewmodel = new AddProviderViewModel
            {
                Id = model.Id,
                ProviderDetails = model.ProviderDetails,
                ImageUrl = model.ImageUrl,
                Name = model.Name
            };

            return View(providerviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddProviderViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Providers");
                        //TODO: investigar borrar imagen anterior
                        _imageHelper.DeleteImageAsync(model.ImageUrl);
                    }
                    var ProviderEntity = _converterHelper.ToProviderEntity(model, path);
                    _context.Providers.Update(ProviderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderEntityExists(model.Id))
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
            if (!ProviderEntityExists(id.Value))
            {
                return NotFound();
            }

            var providerEntity = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (providerEntity == null)
            {
                return NotFound();
            }
            _context.Providers.Remove(providerEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderEntityExists(int id)
        {
            return _context.Providers.Any(e => e.Id == id);
        }
    }
}
