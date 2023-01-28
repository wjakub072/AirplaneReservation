using AirplaneReservation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirplaneReservation.Providers
{
    public interface IDatabaseProvider
    {
        Task<IEnumerable<Reservation>> GetFlightReservationsAsync(int flightId);
        Task<IEnumerable<Flight>> GetFlightsAsync();
    }
}