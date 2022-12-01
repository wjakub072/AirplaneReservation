using AirplaneReservation.Commands;
using AirplaneReservation.Models;
using AirplaneReservation.Services;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;
using System.Data;
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

		public List<AirplaneSeatRowsViewModel> BiznesClassSeatRows { get; set; }



		public ICommand TimetableNavigationCommand { get; }
		public ICommand ConfirmNavigationCommand { get; }
		private ICommand loadSeatConfiguration;
		public ReservationViewModel(INavigationService timetableNavigationService, INavigationService confirmationNavigationService)
		{
			loadSeatConfiguration = new LoadSeatConfiguration(this);
			loadSeatConfiguration.Execute(null);
			//         var esClassSeatRows = new List<List<SeatElementViewModel>>()
			//{ 
			//	new List<SeatElementViewModel> { new SeatElementViewModel() { Number = new string[] { "A1", "A2" } }, new SeatElementViewModel() { Number = new string[] { "A1", "A2" } } },
			//	new List<SeatElementViewModel> { new SeatElementViewModel() { Number = new string[] { "A1", "A2" } }, new SeatElementViewModel() { Number = new string[] { "A1", "A2" } } } 
			//};

            TimetableNavigationCommand = new TimetableNavigationCommand(timetableNavigationService);
            ConfirmNavigationCommand = new ConfirmNavigationCommand(confirmationNavigationService);
		}

    }
}
