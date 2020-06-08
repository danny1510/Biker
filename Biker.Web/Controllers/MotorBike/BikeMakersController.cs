﻿using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Helpers;
using Biker.Web.Models;
using Biker.Web.Models.MotorBike;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Biker.Web.Controllers.MotorBike
{
    [Authorize(Roles = "Admin")]
    public class BikeMakersController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public BikeMakersController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public IActionResult Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(_context.BikeMakers
                .OrderBy(m => m.Name));

        }

        public async Task<IActionResult> Details(int? id, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            if (id == null)
            {
                return NotFound();
            }

            var bikeMakerEntity = await _context.BikeMakers
                .Include(bm => bm.TypeMaker)
                .ThenInclude(tm=> tm.Type)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bikeMakerEntity == null)
            {
                return NotFound();
            }

            return View(bikeMakerEntity);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBikeMakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Makers");
                }

                var bikeMakerEntity = _converterHelper.ToBikeMakerEntity(model, path);

                try
                {
                    _context.BikeMakers.Add(bikeMakerEntity);
                }
                catch (System.Exception err)
                {
                    ModelState.AddModelError(string.Empty, err.Message);
                    return View(model);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: BikeMakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.BikeMakers.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var bikeMakerModel = _converterHelper.ToBikeMakerViewModel(model);

            if (bikeMakerModel == null)
            {
                return NotFound();
            }
            return View(bikeMakerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddBikeMakerViewModel model)
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
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Makers");
                    //TODO: investigar
                    _imageHelper.DeleteImageAsync(model.ImageUrl);
                }

                var bikeMakerEntity = _converterHelper.ToBikeMakerEntity(model, path);

                try
                {
                    _context.BikeMakers.Update(bikeMakerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException err)
                {
                    ModelState.AddModelError(string.Empty, err.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: BikeMakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikeMakerEntity = await _context.BikeMakers
                .Include(m => m.Motorbikes)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (bikeMakerEntity == null)
            {
                return NotFound();
            }

            if (bikeMakerEntity.Motorbikes.Count > 0)
            {

                var error = "The Motorbike Maker can't be deleted because it has related records.";
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeMakers", error }));
            }

            try
            {
                _context.BikeMakers.Remove(bikeMakerEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                ModelState.AddModelError(string.Empty, err.Message);
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index));


        }

        private bool BikeMakerEntityExists(int id)
        {
            return _context.BikeMakers.Any(e => e.Id == id);
        }


        public async Task<IActionResult> AddType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maker = await _context.BikeMakers.FindAsync(id.Value);
            //.Include(bm => bm.Motorbikes)
            //.Include(bm => bm.TypeMaker)
            //.FirstOrDefaultAsync(bm => bm.Id == id.Value);

            if (maker == null)
            {
                return NotFound();
            }

            var typeMakermodel = new TypeMakerViewModel
            {
                Types = _combosHelper.GetComboTypes(),
                MakerId = maker.Id,
                Maker = maker
            };

            return View(typeMakermodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddType(TypeMakerViewModel model)
        {

    
            model.Maker = await _context.BikeMakers.FindAsync(model.MakerId);
            model.Type = await _context.BikeTypes.FindAsync(model.TypeId);

            if (model.Maker == null)
            {
                model.Types = _combosHelper.GetComboTypes();
                return View(model);
            }

            if (model.Type == null)
            {
                model.Types = _combosHelper.GetComboTypes();
                return View(model);
            }


            var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "BikeTypes");
                }

                var typeMakerEntity = new TypeMakerEntity
                {
                    ImageUrl = path,
                    Maker = await _context.BikeMakers.FindAsync(model.MakerId),
                    Type = await _context.BikeTypes.FindAsync(model.TypeId)
                };

                try
                {
                    _context.TypeMakers.Add(typeMakerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    return RedirectToAction($"Details/{model.MakerId}", new RouteValueDictionary(new { Controller = "BikeMakers", err.Message }));
                }

                return RedirectToAction($"Details/{model.MakerId}");
            

            
        }




    }
}
