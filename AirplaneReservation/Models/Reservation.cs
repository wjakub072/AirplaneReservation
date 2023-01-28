using System.Collections.Generic;

namespace AirplaneReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
    }
}
