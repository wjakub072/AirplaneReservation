using AirplaneReservation.Commands;
using AirplaneReservation.Models;
using AirplaneReservation.Services;
using System.Windows.Input;

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

		public string FlightInformation
        {
			get
			{
				return string.Concat("LOT: ", _selectedFlight.DisplayValue.Replace("\n", " "));
			}
		}


		public ICommand TimetableNavigationCommand { get; }
		public ICommand ConfirmNavigationCommand { get; }
		public ReservationViewModel(INavigationService timetableNavigationService, INavigationService confirmationNavigationService)
		{


			TimetableNavigationCommand = new TimetableNavigationCommand(timetableNavigationService);
            ConfirmNavigationCommand = new ConfirmNavigationCommand(confirmationNavigationService);
		}

    }
}
