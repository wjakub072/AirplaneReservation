using AirplaneReservation.Commands;
using AirplaneReservation.Factories;
using AirplaneReservation.Models;
using AirplaneReservation.Providers;
using AirplaneReservation.Services;
using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace AirplaneReservation.ViewModels
{
	public sealed class ReservationViewModel : ViewModelBase
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
				return string.Concat("LOT: ", _selectedFlight.Departure.Replace("\n", " "));
			}
		}

		public int AmountOfPassengers { get; set; }

		public List<AirplaneSeatRowViewModel> BiznesClassSeatRows { get; set; }
		public List<AirplaneSeatRowViewModel> EconomicClassSeatRows { get; set; }

		public ICommand TimetableNavigationCommand { get; }
		public ICommand ConfirmNavigationCommand { get; }

		// As loading isn't a command that we attach to any button it's declared private
		private ICommand loadSeatConfiguration;

		public ReservationViewModel()
		{
            ConfirmNavigationCommand = new ConfirmNavigationCommand(
               null, null,null,
               this);
            loadSeatConfiguration = new LoadSeatConfiguration(
                new DatabaseProvider(),
                this);

			SelectedFlight = new Flight()
			{
				EconomicRowsInPlane = 4,
				EconomicSeatsInRow = 16
			};
            EconomicClassSeatRows = new List<AirplaneSeatRowViewModel>();
            for (int i = 0; i < SelectedFlight.EconomicRowsInPlane; i++)
            {
                AirplaneSeatRowViewModel row = new AirplaneSeatRowViewModel();
                row.SeatsInRow = new AirplaneSeatCellViewModel[SelectedFlight.EconomicSeatsInRow];
                for (int j = 0; j < SelectedFlight.EconomicSeatsInRow; j++)
                {
                    var seatCell = new AirplaneSeatCellViewModel()
                    {
                        Column = j,
                        Row = i,
                        Selected = false,
                        StateBrush = Brushes.White
                    };

                    row.SeatsInRow[j] = seatCell;
                }
                EconomicClassSeatRows.Add(row);
            }

            BiznesClassSeatRows = new List<AirplaneSeatRowViewModel>();
            for (int i = 0; i < SelectedFlight.BiznesRowsInPlane; i++)
            {
                AirplaneSeatRowViewModel row = new AirplaneSeatRowViewModel();
                row.SeatsInRow = new AirplaneSeatCellViewModel[SelectedFlight.BiznesSeatsInRow];
                for (int j = 0; j < SelectedFlight.BiznesSeatsInRow; j++)
                {
                    var seatCell = new AirplaneSeatCellViewModel()
                    {
                        Column = j,
                        Row = i,
                        Selected = false,
                        StateBrush = Brushes.White
                    };

                    row.SeatsInRow[j] = seatCell;
                }
                BiznesClassSeatRows.Add(row);
            }
        }
        internal ReservationViewModel(INavigationService timetableNavigationService, 
			INavigationService confirmationNavigationService,
			IReservationFactory reservationFactory, 
			IDatabaseAccessService databaseAccessService, 
			IDatabaseProvider databaseProvider)
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
