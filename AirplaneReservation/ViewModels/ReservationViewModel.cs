using AirplaneReservation.Models;

namespace AirplaneReservation.ViewModels
{
    internal class ReservationViewModel : ViewModelBase
    {
		private Flight _selectedFlight;

		public Flight SelectedFlight
		{
			get { return _selectedFlight; }
			set { _selectedFlight = value; }
		}

	}
}
