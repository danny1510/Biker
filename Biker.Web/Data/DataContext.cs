﻿using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyVet.Web.Data.Entities;

namespace Biker.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<MotorbikeEntity> Motorbikes { get; set; }
        public DbSet<BikeMakerEntity> BikeMakers { get; set; }
        public DbSet<BikeTypeEntity>  BikeTypes  { get; set; }
        public DbSet<TypeMakerEntity> TypeMakers { get; set; }
        public DbSet<BikerEntity>     Bikers     { get; set; }
        public DbSet<ManagerEntity>   Managers { get; set; }



    }
}