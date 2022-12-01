using System.Collections.Generic;

namespace AirplaneReservation.Models
{
    internal class Reservation
    {
        public IEnumerable<Seat> Seats { get; set; }
    }
}
