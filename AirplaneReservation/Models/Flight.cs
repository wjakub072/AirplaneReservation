using System;

namespace AirplaneReservation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int BiznesSeatsInRow { get; set; }
        public int BiznesRowsInPlane { get; set; }
        public int EconomicSeatsInRow { get; set; }
        public int EconomicRowsInPlane { get; set; }
    }
}
