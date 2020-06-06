using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Helpers;
using Biker.Web.Models;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index()
        {
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

            var bikerEntity = await _context.Bikers.FindAsync(id);
            if (bikerEntity == null)
            {
                return NotFound();
            }
            return View(bikerEntity);
        }


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
