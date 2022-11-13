using AirplaneReservation.Commands.PassengerAmountCommands;
using AirplaneReservation.Commands;
using AirplaneReservation.Models;
using AirplaneReservation.Services;
using System.Windows.Input;

namespace AirplaneReservation.ViewModels
{
	internal class PassengerAmountViewModel : ViewModelBase
    {
		private int _passengerAmount = 1;

		public int PassengerAmount
        {
			get { return _passengerAmount; }
			set { 
				_passengerAmount = value;
				OnPropertyChanged(nameof(PassengerAmount));
			}
		}

		public Flight SelectedFlight { get; set; }

		public ICommand LessPassengersCommand { get; }
		public ICommand MorePassengersCommand { get; }
		public ICommand TimetableNavigationCommand { get; }
		public ICommand ReservationNavigationCommand { get; }

		public PassengerAmountViewModel(INavigationService timetableNavigationService, IParameterNavigationService reservationNavigationCommand)
		{
			LessPassengersCommand = new LessPassengersCommand(this);
			MorePassengersCommand = new MorePassengersCommand(this);

			TimetableNavigationCommand = new TimetableNavigationCommand(timetableNavigationService);
			ReservationNavigationCommand = new ReservationNavigationCommand(this, reservationNavigationCommand);
		}
    }
}
