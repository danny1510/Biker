using Biker.Web.Data.Entities;
using MyVet.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task SeedAsync()
        {
            //Garantizar que la base de datos esté creada sino crearla
            await _dataContext.Database.EnsureCreatedAsync();

            await CheckBikeMakerAsync();
            await CheckBikeTypeAsync();
            await CheckBiker();

        }

        private async Task CheckBiker()
        {
            if (!_dataContext.Bikers.Any())
            {
                await _dataContext.Bikers.AddAsync(new BikerEntity { 
                    FirstName = "Danny",   
                    LastName = "Gaviria",
                    Address = "Calle 80 sur",
                    CellPhone = "3196566541" });

                await _dataContext.Bikers.AddAsync(new BikerEntity {
                    FirstName = "Valntina",
                    LastName = "Múneraa",
                    Address = "Toscana",
                    CellPhone = "3117080895" });

                await _dataContext.Bikers.AddAsync(new BikerEntity { 
                    FirstName = "Andres",
                    LastName = "Vargas",
                    Address = "Calasanz",
                    CellPhone = "3206509726" });

            }
            await _dataContext.SaveChangesAsync();

        }

        private async Task CheckBikeMakerAsync()
        {
            if (!_dataContext.BikeMakers.Any())
            {
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "YAMAHA"   });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "HONDA"    });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "DUCATI"   });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "SUZUKI"   });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "KAWASAKI" });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "KTM"      });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "AUTECO"   });
                await _dataContext.BikeMakers.AddAsync(new BikeMakerEntity { Name = "AKT"      });

                await _dataContext.SaveChangesAsync();
            }

        }

        private async Task CheckBikeTypeAsync()
        {
            if (!_dataContext.BikeTypes.Any())
            {
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "ENDURO"                     });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "URBANA"                     });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "NAKED"                      });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "DEPORTIVA"                  });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "SUPER DEPORTIVA"            });
                await _dataContext.BikeTypes.AddAsync(new BikeTypeEntity { Name = "AUTOMÁTICA Y SEMIAUTOMÁTICA"});
                await _dataContext.SaveChangesAsync();
            }
        }

    }
}
