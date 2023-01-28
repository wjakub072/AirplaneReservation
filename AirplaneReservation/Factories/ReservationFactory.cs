using AirplaneReservation.Models;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;

namespace AirplaneReservation.Factories
{
    public class ReservationFactory : IReservationFactory
    {
        public Reservation CreateReservation(IEnumerable<AirplaneSeatCellViewModel> selectedSeats, int flightId)
        {
            Reservation reservation = new Reservation();
            List<Seat> reservationSeats = new List<Seat>();
            foreach (var seat in selectedSeats)
            {
                reservationSeats.Add(new Seat()
                {
                    Row = seat.Row,
                    Column = seat.Column
                });
            }
            reservation.FlightId = flightId;
            reservation.Seats = reservationSeats;

            return reservation;
        }
    }
}
