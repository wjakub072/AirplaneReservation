using AirplaneReservation.Database;
using AirplaneReservation.Models;
using AirplaneReservation.Services.Interfaces;
using System.Threading.Tasks;

namespace AirplaneReservation.Services
{
    public sealed class DatabaseAccessService : IDatabaseAccessService
    {
        private readonly ApplicationDbContext _db;

        public DatabaseAccessService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddReservation(Reservation newReservation)
        {
            _db.Reservations.Add(newReservation);
            await _db.SaveChangesAsync();
        }
    }
}
