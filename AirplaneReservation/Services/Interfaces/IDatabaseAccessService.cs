using AirplaneReservation.Models;
using System.Threading.Tasks;

namespace AirplaneReservation.Services.Interfaces
{
    internal interface IDatabaseAccessService
    {
        Task AddReservation(Reservation newReservation);
    }
}