using AirplaneReservation.Models;
using System.Threading.Tasks;

namespace AirplaneReservation.Services.Interfaces
{
    public interface IDatabaseAccessService
    {
        Task AddReservation(Reservation newReservation);
    }
}