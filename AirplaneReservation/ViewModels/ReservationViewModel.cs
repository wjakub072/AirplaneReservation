using AirplaneReservation.Commands;
using AirplaneReservation.Factories;
using AirplaneReservation.Models;
using AirplaneReservation.Providers;
using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;
using System.Windows.Input;

namespace AirplaneReservation.ViewModels
{
	internal sealed class ReservationViewModel : ViewModelBase
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

		public int AmountOfPassengers { get; set; }

		public List<AirplaneSeatRowViewModel> BiznesClassSeatRows { get; set; }
		public List<AirplaneSeatRowViewModel> EconomicClassSeatRows { get; set; }

		public ICommand TimetableNavigationCommand { get; }
		public ICommand ConfirmNavigationCommand { get; }

		// As loading isn't a command that we attach to any button it's declared private
		private ICommand loadSeatConfiguration;

		public ReservationViewModel(INavigationService timetableNavigationService, INavigationService confirmationNavigationService, IReservationFactory reservationFactory, IDatabaseAccessService databaseAccessService, IDatabaseProvider databaseProvider)
		{
			loadSeatConfiguration = new LoadSeatConfiguration(
				databaseProvider, 
				this);
			loadSeatConfiguration.Execute(null);

            TimetableNavigationCommand = new TimetableNavigationCommand(
				timetableNavigationService);

            ConfirmNavigationCommand = new ConfirmNavigationCommand(
				confirmationNavigationService, 
				reservationFactory, 
				databaseAccessService, 
				this);
		}

    }
}
