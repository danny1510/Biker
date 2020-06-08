using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Helpers;
using Biker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ManagersController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Managers.Include(m => m.UserEntity));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerEntity = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (managerEntity == null)
            {
                return NotFound();
            }

            return View(managerEntity);
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
                    await _userHelper.AddUserToRoleAsync(userinDB, "Admin");

                    var admin = new ManagerEntity
                    {
                        UserEntity = userinDB
                    };

                    _context.Managers.Add(admin);

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



            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerEntity = await _context.Managers
                .Include(b => b.UserEntity)
                .FirstOrDefaultAsync(b => b.Id == id.Value);

            if (managerEntity == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserViewModel
            {
                Id = managerEntity.Id,
                Address = managerEntity.UserEntity.Address,
                FirstName = managerEntity.UserEntity.FirstName,
                LastName = managerEntity.UserEntity.LastName,
                PhoneNumber = managerEntity.UserEntity.PhoneNumber
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

            var managerEntity = await _context.Managers
            .Include(m => m.UserEntity)
            .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (managerEntity  == null)
            {
                return NotFound();
            }

            managerEntity.UserEntity.FirstName = model.FirstName;
            managerEntity.UserEntity.LastName = model.LastName;
            managerEntity.UserEntity.Address = model.Address;
            managerEntity.UserEntity.PhoneNumber = model.PhoneNumber;

            try
            {
                await _userHelper.UpdateUserAsync(managerEntity.UserEntity);
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

            var managerEntity = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (managerEntity == null)
            {
                return NotFound();
            }

            return View(managerEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var managerEntity = await _context.Managers.FindAsync(id);
            _context.Managers.Remove(managerEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerEntityExists(int id)
        {
            return _context.Managers.Any(e => e.Id == id);
        }
    }
}
