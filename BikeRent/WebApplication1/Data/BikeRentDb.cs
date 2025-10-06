
using Microsoft.EntityFrameworkCore;
using BikeRent.Models;
using BikeRent.Events;
using System;
using Microsoft.Exchange.WebServices.Data;

namespace BikeRent.Data
{
    public class BikeRentDb : DbContext
    {
        public BikeRentDb(DbContextOptions<BikeRentDb> options) : base(options) { }
        public DbSet<Biker> Bikers => Set<Biker>();
        public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
        public DbSet<Rent> Rents=> Set<Rent>();
        public DbSet<MotorcycleEvent2024> MotorcyclesEvent2024 => Set<MotorcycleEvent2024>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biker>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Motorcycle>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Rent>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
