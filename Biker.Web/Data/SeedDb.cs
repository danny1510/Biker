using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
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
            if (!_dataContext.UserRoles.Any())
            {


                await CheckRolesAsync();
                var manager = await CheckUserAsync("Danny", "Gaviria", "Dannygaviria111@gmail.com", "Calle 80 sur", "3196566541", "Admin");
                var customer = await CheckUserAsync("Valentina", "Múnera", "valenm@gmail.com", "Toscana", "3117080895", "Customer");
                var customer2 = await CheckUserAsync("Andres", "Vargas", "andres@gmail.com", "Toscana", "3117080895", "Customer");
                await CheckManagerAsync(manager);
                var biker = await CheckcustomerAsync(customer);
                var biker2 = await CheckcustomerAsync(customer2);


                var maker = await CheckBikeMakerAsync("YAMAHA");
                var maker2 = await CheckBikeMakerAsync("HONDA");
                var maker3 = await CheckBikeMakerAsync("DUCATI");
                var maker4 = await CheckBikeMakerAsync("SUZUKI");
                var maker5 = await CheckBikeMakerAsync("KAWASAKI");
                var maker6 = await CheckBikeMakerAsync("KTM");
                var maker7 = await CheckBikeMakerAsync("AUTECO");
                var maker8 = await CheckBikeMakerAsync("AKT");
                var type = await CheckBikeTypeAsync("ENDURO");
                var type2 = await CheckBikeTypeAsync("URBANA");
                var type3 = await CheckBikeTypeAsync("NAKED");
                var type4 = await CheckBikeTypeAsync("DEPORTIVA");
                var type5 = await CheckBikeTypeAsync("SUPER DEPORTIVA");
                var type6 = await CheckBikeTypeAsync("AUTO Y SEMI");

                var motorbike = await CheckMotorBikeAsync("XTZ", 250, true, 90, 90, 21, 120, 80, 18, maker, type);
                var motorbike2 = await CheckMotorBikeAsync("BOXER", 100, false, 275, 0, 17, 300, 0, 17, maker7, type2);
                var motorbike3 = await CheckMotorBikeAsync("MT09", 900, true, 120, 70, 17, 180, 55, 17, maker, type4);
                var motorbike4 = await CheckMotorBikeAsync("XT660Z", 660, true, 90, 90, 21, 130, 80, 17, maker, type);

                var bikespare = await CheckMotorBikeSpareAsync(2015, 2019, motorbike);
                var bikespare2 = await CheckMotorBikeSpareAsync(2019, 0, motorbike);
                var bikespare3 = await CheckMotorBikeSpareAsync(2002, 0, motorbike2);
                var bikespare4 = await CheckMotorBikeSpareAsync(2015, 0, motorbike3);

                await CkeckBikermotorAsync(biker, bikespare);
                await CkeckBikermotorAsync(biker2, bikespare2);

            }
        }


        private async Task<BikerEntity> CheckcustomerAsync(UserEntity customer)
        {

            var biker = new BikerEntity
            {
                UserEntity = customer,
                BikerMotors = new List<BikerMotorEntity>()
            };
            _dataContext.Bikers.Add(biker);

            await _dataContext.SaveChangesAsync();

            return biker;
        }

        private async Task CheckManagerAsync(UserEntity manager)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new ManagerEntity { UserEntity = manager });
                await _dataContext.SaveChangesAsync();
            }
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

        private async Task<BikeMakerEntity> CheckBikeMakerAsync(string name)
        {

            var maker = new BikeMakerEntity { Name = name };
            await _dataContext.BikeMakers.AddAsync(maker);
            await _dataContext.SaveChangesAsync();

            return maker;

        }

        private async Task<BikeTypeEntity> CheckBikeTypeAsync(string name)
        {

            var type = new BikeTypeEntity { Name = name };
            await _dataContext.BikeTypes.AddAsync(type);
            await _dataContext.SaveChangesAsync();
            return type;

        }

        private async Task<MotorBikeEntity> CheckMotorBikeAsync(string name, int cylinder, bool millimeters,
            int orgw_ft, int orgh_ft, int fronttire, int orgw_rt, int orgh_rt, int reartire, BikeMakerEntity maker, BikeTypeEntity type)
        {

            var motorbike = new MotorBikeEntity
            {
                Name = name,
                Cylinder = cylinder,
                Millimeters = millimeters,
                WidthTireF = orgw_ft,
                HeightTireF = orgh_ft,
                FrontTire = fronttire,
                WidthTireR = orgw_rt,
                HeightTireR = orgh_rt,
                RearTire = reartire,
                BikeMaker = maker,
                BikeType = type,
                MotorBikeSpares = new List<MotorBikeSpareEntity>(),

            };
            await _dataContext.MotorBikes.AddAsync(motorbike);
            await _dataContext.SaveChangesAsync();

            return motorbike;
        }

        private async Task<MotorBikeSpareEntity> CheckMotorBikeSpareAsync(int yearsince, int until,
            MotorBikeEntity motorBike
            )
        {
            var bikespare = new MotorBikeSpareEntity
            {
                YearSince = yearsince,
                YearUntil = until,
                MotorBike = motorBike,
                BikerMotors = new List<BikerMotorEntity>()
            };

            _dataContext.MotorBikeSpares.Add(bikespare);

            await _dataContext.SaveChangesAsync();

            return bikespare;
        }



        private async Task CkeckBikermotorAsync(BikerEntity biker, MotorBikeSpareEntity bikespare)
        {
            var bikermotor = new BikerMotorEntity
            {
                Biker = biker,
                MotorBikeSpare = bikespare
            };

            await _dataContext.BikerMotors.AddAsync(bikermotor);

            await _dataContext.SaveChangesAsync();

        }
    }
}
