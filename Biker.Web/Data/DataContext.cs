using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.Biker;
using Biker.Web.Data.Entities.MotorBike;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Biker.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<MotorBikeEntity>      MotorBikes { get; set; }
        public DbSet<MotorBikeSpareEntity> MotorBikeSpares { get; set; }
        public DbSet<BikeMakerEntity>      BikeMakers { get; set; }
        public DbSet<BikeTypeEntity>       BikeTypes  { get; set; }
        public DbSet<TypeMakerEntity>      TypeMakers { get; set; }
        public DbSet<BikerEntity>          Bikers     { get; set; }
        public DbSet<ManagerEntity>        Managers { get; set; }
        public DbSet<BikerMotorEntity>     BikerMotors { get; set; }




    }
}