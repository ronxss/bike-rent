
using Microsoft.EntityFrameworkCore;
using BikeRent.Models;
using System;

namespace BikeRent.Data
{
    public class BikeRentDb : DbContext
    {
        public BikeRentDb(DbContextOptions<BikeRentDb> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
        public DbSet<Biker> Bikers => Set<Biker>();
        public DbSet<Location> Locations => Set<Location>();
    }
}
