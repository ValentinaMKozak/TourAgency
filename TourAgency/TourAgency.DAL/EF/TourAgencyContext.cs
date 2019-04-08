using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.DAL.Entities;

namespace TourAgency.DAL.EF
{
    public class TourAgencyContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<DailyProgram> DailyPrograms { get; set; }
        public DbSet<Order> Orders { get; set; }

        public TourAgencyContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourCountry>()
                .HasKey(k => new { k.TourId, k.CountryId });

            modelBuilder.Entity<TourCountry>()
                .HasOne(t => t.Tour)
                .WithMany(tc => tc.TourCountries)
                .HasForeignKey(t => t.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TourCountry>()
                .HasOne(c => c.Country)
                .WithMany(tc => tc.TourCountries)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TourAgencyIdentityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
