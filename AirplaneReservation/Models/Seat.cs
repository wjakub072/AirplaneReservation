namespace AirplaneReservation.Models
{
    internal class Seat
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Column { get; set; }
        public char Row { get; set; }
    }
}
