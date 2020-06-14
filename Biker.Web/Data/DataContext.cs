using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Data.Entities.Provider;
using Biker.Web.Data.Entities.Spare;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Biker.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Motos y configuración
        public DbSet<MotorBikeEntity>      MotorBikes { get; set; }
        public DbSet<MotorBikeSpareEntity> MotorBikeSpares { get; set; }
        public DbSet<BikeMakerEntity>      BikeMakers { get; set; }
        public DbSet<BikeTypeEntity>       BikeTypes  { get; set; }
        public DbSet<TypeMakerEntity>      TypeMakers { get; set; }
        public DbSet<BikerEntity>          Bikers     { get; set; }
        public DbSet<ManagerEntity>        Managers { get; set; }
        public DbSet<BikerMotorEntity>     BikerMotors { get; set; }

        //Repuestos
        public DbSet<BikeSpareEntity> BikeSpares { get; set; }
        public DbSet<SpareEntity> Spares { get; set; }
        public DbSet<SpareProviderEntity> SpareProviders { get; set; }
        public DbSet<ProviderEntity> Providers { get; set; }
        public DbSet<ProviderDetailsEntity> ProviderDetails { get; set; }

        public DbSet<SpareCategoryEntity> SpareCategories { get; set; }
        public DbSet<SpareBrandEntity> SpareBrands  { get; set; }
        public DbSet<BrandCategoryEntity> BrandCategories { get; set; }









    }
}