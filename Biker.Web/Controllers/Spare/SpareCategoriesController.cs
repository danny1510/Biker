using Biker.Web.Data;
using Biker.Web.Data.Entities.Spare;
using Biker.Web.Helpers;
using Biker.Web.Models.Spare;
using Biker.Web.Models.Spare.List;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.Spare
{
    public class SpareCategoriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public SpareCategoriesController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        public IActionResult Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(_context.SpareCategories
                .Include(sc => sc.BrandCategories)
                .ThenInclude(bc => bc.SpareBrand)
                .OrderBy(m => m.Name)
                );

            //return View(await _context.SpareCategories.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spareCategoryEntity = await _context.SpareCategories
                .Include(sc => sc.BrandCategories)
                  .ThenInclude(bc => bc.SpareBrand)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (spareCategoryEntity == null)
            {
                return NotFound();
            }

            return View(spareCategoryEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSpareCategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var spareCategories = await _context.SpareCategories.FirstOrDefaultAsync
                (sc => sc.Name.ToUpper() == model.Name.ToUpper());

                if (spareCategories != null)
                {
                    var error = "The Spare Catergory can't be Added because it already exists.";
                    //return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "SpareCategories", error }));
                    ModelState.AddModelError(string.Empty, error);
                    return View(model);
                }

                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "SpareCategories");
                }

                spareCategories = _converterHelper.ToSpareCategoryEntity(model, path);

                _context.SpareCategories.Add(spareCategories);
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

            var model = await _context.SpareCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var sparecategoryviewmodel = new AddSpareCategoriesViewModel
            {
                Id = model.Id,
                BrandCategories = model.BrandCategories,
                ImageUrl = model.ImageUrl,
                Name = model.Name
            };

            return View(sparecategoryviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddSpareCategoriesViewModel model)
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
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "SpareCategories");
                    //TODO: investigar borrar imagen anterior
                    _imageHelper.DeleteImageAsync(model.ImageUrl);
                }

                var sparecategoryEntity = _converterHelper.ToSpareCategoryEntity(model, path);

                try
                {
                    _context.SpareCategories.Update(sparecategoryEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpareCategoryEntityExists(model.Id))
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

            if (!SpareCategoryEntityExists(id.Value))
            {
                return NotFound();
            }

            var spareCategoryEntity = await _context.SpareCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (spareCategoryEntity == null)
            {
                return NotFound();
            }

            _context.SpareCategories.Remove(spareCategoryEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool SpareCategoryEntityExists(int id)
        {
            return _context.SpareCategories.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddBrand(int id)
        {
            var model = await _context.SpareCategories.FindAsync(id);
            var name_category = model.Name;
            var ImgUrlCat = model.ImageUrl;
            var allbrand = _context.SpareBrands.Select(sb => new SpareBrandList()
            {
                Id_Brand = sb.Id,
                Name = sb.Name,
                ImageUrl = sb.ImageUrl,
                Is_checked = sb.BrandCategories.Any(bc => bc.SpareCategory.Id == id) ? true : false,
                NameCategory = name_category,
                ImgUrlCategory = ImgUrlCat,
                SpareCategoryId = id
            })
             .OrderBy(sb => sb.Name)
             .OrderByDescending(sb => sb.Is_checked)
             .ToList();

            return View(allbrand);

        }

        public async Task<IActionResult> AddBrand2(int id)
        {
            SpareCategoryList scl = new SpareCategoryList();

            var model = await _context.SpareCategories.FindAsync(id);
         
            var allbrands = _context.SpareBrands.Select(sb => new SpareBrandList2()
            {
                Id_Brand = sb.Id,
                NameBrand = sb.Name,
                ImageUrlBrand = sb.ImageUrl,
                Is_checkedBrand = sb.BrandCategories.Any(bc => bc.SpareCategory.Id == id) ? true : false

            })
             .OrderBy(sb => sb.NameBrand)
             .OrderByDescending(sb => sb.Is_checkedBrand)
             .ToList();

            scl.NameCategory = model.Name;
            scl.ImgUrlCategory = model.ImageUrl;
            scl.IdCategory = model.Id;
            scl.SpareBrands = allbrands;


            return View(scl);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBrand2(SpareCategoryList model)
        {
            if (model.SpareBrands != null)
            {

                List<BrandCategoryEntity> brandCategory = new List<BrandCategoryEntity>();

                foreach (var item in model.SpareBrands)
                {
                    if (item.Is_checkedBrand == true)
                    {
                        brandCategory.Add(new BrandCategoryEntity()
                        {
                            SpareBrand = await _context.SpareBrands.FindAsync(item.Id_Brand),
                            SpareCategory = await _context.SpareCategories.FindAsync(model.IdCategory)
                        });
                    }
                }

                var table = _context.BrandCategories.Where(bc => bc.SpareCategory.Id == model.IdCategory);
                var list = table.Except(brandCategory).ToList();

                foreach (var item in list)
                {
                    _context.BrandCategories.Remove(item);
                    await _context.SaveChangesAsync();
                }

                table = _context.BrandCategories.Where(bc => bc.SpareCategory.Id == model.IdCategory);
                foreach (var item in brandCategory)
                {
                    if (!table.Contains(item))
                    {
                        await _context.BrandCategories.AddAsync(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction($"Details/{model.IdCategory}");
        }




    }
}

