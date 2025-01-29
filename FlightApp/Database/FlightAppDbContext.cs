using FlightApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Database
{
    public class FlightAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightNote> FlightNotes { get; set; }

        public FlightAppDbContext(DbContextOptions<FlightAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
