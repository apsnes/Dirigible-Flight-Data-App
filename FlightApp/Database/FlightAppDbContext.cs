using FlightApp.Models;
using FlightApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace FlightApp.Database
{
    public class FlightAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Note> Notes { get; set; }

        public FlightAppDbContext(DbContextOptions<FlightAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" });
        }
    }
}
