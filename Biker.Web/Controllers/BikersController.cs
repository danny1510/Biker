using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Helpers;
using Biker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers
{
    public class BikersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public BikersController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IActionResult Index(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(_context.Bikers
                .Include(b => b.BikerMotors)
                .Include(b => b.UserEntity));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers
                .Include(b => b.UserEntity)
                .Include(b => b.BikerMotors)
                .ThenInclude(bm => bm.MotorBikeSpare)
                .ThenInclude(mbs => mbs.MotorBike)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bikerEntity == null)
            {
                return NotFound();
            }

            return View(bikerEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Email = model.Username,
                    UserName = model.Username,
                    PhoneNumber = model.PhoneNumber
                };

                var response = await _userHelper.AddUserAsync(user, model.Password);

                if (response.Succeeded)
                {
                    var userinDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userinDB, "Customer");

                    var biker = new BikerEntity
                    {
                        UserEntity = userinDB,
                        BikerMotors = new List<BikerMotorEntity>()
                    };

                    _context.Bikers.Add(biker);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return RedirectToAction(nameof(Index));
                    }

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);

            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers
                .Include(b => b.UserEntity)
                .FirstOrDefaultAsync(b => b.Id == id.Value);

            if (bikerEntity == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserViewModel
            {
                Id = bikerEntity.Id,
                Address = bikerEntity.UserEntity.Address,
                FirstName = bikerEntity.UserEntity.FirstName,
                LastName = bikerEntity.UserEntity.LastName,
                PhoneNumber = bikerEntity.UserEntity.PhoneNumber
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserViewModel model)
        {
            if (id != model.Id || id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var bikerEntity = await _context.Bikers
             .Include(b => b.UserEntity)
             .FirstOrDefaultAsync(b => b.Id == model.Id);

            if (bikerEntity == null)
            {
                return NotFound();
            }

            bikerEntity.UserEntity.FirstName = model.FirstName;
            bikerEntity.UserEntity.LastName = model.LastName;
            bikerEntity.UserEntity.Address = model.Address;
            bikerEntity.UserEntity.PhoneNumber = model.PhoneNumber;

            try
            {
                await _userHelper.UpdateUserAsync(bikerEntity.UserEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException err)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeMakers", err.Message }));
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bikerEntity = await _context.Bikers
                .Include(b => b.UserEntity)
                .Include(b => b.BikerMotors)
                .FirstOrDefaultAsync(b => b.Id == id.Value);

            if (bikerEntity == null)
            {
                return NotFound();
            }

            if (bikerEntity.BikerMotors.Count > 0)
            {
                var error = "The Biker can't be deleted because it has related records.";
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeMakers", error }));
            }

            try
            {
                _context.Bikers.Remove(bikerEntity);
                _context.Users.Remove(bikerEntity.UserEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "BikeMakers", err.Message }));

            }

            return RedirectToAction(nameof(Index));
        }

        private bool BikerEntityExists(int id)
        {
            return _context.Bikers.Any(e => e.Id == id);
        }
    }
}
