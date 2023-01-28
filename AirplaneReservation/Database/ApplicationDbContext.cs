using AirplaneReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace AirplaneReservation.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Seat> Seats => Set<Seat>();
    }
}
