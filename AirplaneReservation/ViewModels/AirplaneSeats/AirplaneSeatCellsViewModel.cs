using System.Windows.Input;

namespace AirplaneReservation.ViewModels.AirplaneSeats
{
    internal class AirplaneSeatCellsViewModel
    {
        public bool Reserved { get; set; }
        public bool Selected { get; set; }
        public string Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int MyProperty { get; set; }
        public ICommand SelectSeatCommand { get; set; }
    }
}
