using Biker.Web.Data.Entities;
using Biker.Web.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using MyVet.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            //Garantizar que la base de datos esté creada sino crearla
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            var manager = await CheckUserAsync("Danny", "Gaviria", "Dannygaviria111@gmail.com", "Calle 80 sur", "3196566541", "Admin");
            var customer = await CheckUserAsync("Valentina", "Múnera", "valenm@gmail.com", "Toscana", "3117080895", "Customer");
            await CheckManagerAsync(manager);
            await CheckcustomerAsync(customer);
            await CheckBikeMakerAsync();
            await CheckBikeTypeAsync();

        }

        private async Task CheckcustomerAsync(UserEntity customer)
        {
            if (!_dataContext.Bikers.Any())
            {
                _dataContext.Bikers.Add(new BikerEntity { UserEntity = customer });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(UserEntity manager)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new ManagerEntity { UserEntity = manager });
                await _dataContext.SaveChangesAsync();
            }
        }

        private Task CheckUserAsync(string v1, string v2, string v3, string v4, string v5)
        {
            throw new NotImplementedException();
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<UserEntity> CheckUserAsync(string firstName, string lastName, string email, string address, string phoneNumber, string role)
        {

            var user = await _userHelper.GetUserByEmailAsync(email);

            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    Address = address,
                    PhoneNumber = phoneNumber
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

            }

            return user;

        }

        private async Task CheckBikeMakerAsync()
        {
            if (!_dataContext.BikeMakers.Any())
            {
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "YAMAHA" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "HONDA" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "DUCATI" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "SUZUKI" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "KAWASAKI" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "KTM" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "AUTECO" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "AKT" });

                await _dataContext.SaveChangesAsync();
            }

        }

        private async Task CheckBikeTypeAsync()
        {
            if (!_dataContext.BikeTypes.Any())
            {
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "ENDURO" });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "URBANA" });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "NAKED" });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "DEPORTIVA" });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "SUPER DEPORTIVA" });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "AUTOMÁTICA Y SEMIAUTOMÁTICA" });
                await _dataContext.SaveChangesAsync();
            }
        }

    }
}
