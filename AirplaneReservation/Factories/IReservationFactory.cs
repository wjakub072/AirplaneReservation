using AirplaneReservation.Models;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;

namespace AirplaneReservation.Factories
{
    public interface IReservationFactory
    {
        /// <summary>
        /// Make reservation for all selected seats. Method is used for mapping viewModels to DatabaseEntities. 
        /// </summary>
        /// <param name="selectedSeats">Enumerable of all selected seatCells</param>
        /// <param name="flightId">Flight that reservation is referencing</param>
        /// <returns>Reservation DTO</returns>
        Reservation CreateReservation(IEnumerable<AirplaneSeatCellViewModel> selectedSeats, int flightId);
    }
}