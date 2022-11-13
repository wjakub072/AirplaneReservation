using AirplaneReservation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirplaneReservation.Providers
{
    internal sealed class DatabaseProvider : IDatabaseProvider
    {
        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            return null;
        }

        public async Task<IEnumerable<Reservation>> GetFlightReservationsAsync(int flightId)
        {
            return null;
        }
    }
}
