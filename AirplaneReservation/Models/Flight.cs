using System;

namespace AirplaneReservation.Models
{
    internal class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }

        public string DisplayValue
        {
            get
            {
                return string.Concat(From, " - ", To, "\n", Date.ToString("dd.MM hh:mm"));
            }
        }
    }
}
