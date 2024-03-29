﻿using AirplaneReservation.Commands;
using AirplaneReservation.Models;
using AirplaneReservation.Services.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;

namespace AirplaneReservation.ViewModels
{
    internal class TimetableViewModel : ViewModelBase
    {

		private IEnumerable<Flight> _flights;

		public IEnumerable<Flight> Flights
		{
			get { return _flights; }
			set { _flights = value; }
		}

		private Flight _selectedFlight;

		public Flight SelectedFlight
		{
			get { return _selectedFlight; }
			set { 
				_selectedFlight = value;
				OnPropertyChanged(nameof(SelectedFlight));	
			}
		}

		public ICommand PassengerAmountNavigationCommand { get; }


        public TimetableViewModel(IParameterNavigationService passengerAmountNavigationService)
		{
			// populate 
			var flights = new List<Flight>();
			flights.Add(new Flight() { Id = 1, Departure = "Wrocław", Destination = "Londyn", Date = new System.DateTime(2022, 10, 10, 10, 45, 00) });
			flights.Add(new Flight() { Id = 2, Departure = "Wrocław", Destination = "Mediolan", Date = new System.DateTime(2022, 10, 10, 12, 15, 00) });
			flights.Add(new Flight() { Id = 3, Departure = "Wrocław", Destination = "Praga", Date = new System.DateTime(2022, 10, 10, 14, 10, 00) });
		
			_flights = flights;


			PassengerAmountNavigationCommand = new PassengerAmountNavigationCommand(this, passengerAmountNavigationService);
		}
	}
}
