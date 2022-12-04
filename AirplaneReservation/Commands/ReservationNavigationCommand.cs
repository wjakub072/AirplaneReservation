using AirplaneReservation.Models;
using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.ViewModels;
using System;

namespace AirplaneReservation.Commands
{
    internal class ReservationNavigationCommand : CommandBase
    {
        private readonly PassengerAmountViewModel _passengerAmountViewModel;
        private readonly IParameterNavigationService _reservationNavigationCommand;

        public ReservationNavigationCommand(PassengerAmountViewModel passengerAmountViewModel, IParameterNavigationService reservationNavigationCommand)
        {
            _passengerAmountViewModel = passengerAmountViewModel;
            _reservationNavigationCommand = reservationNavigationCommand;
        }

        public override void Execute(object parameter)
        {
            _reservationNavigationCommand.Navigate(new Tuple<Flight, int>(_passengerAmountViewModel.SelectedFlight, _passengerAmountViewModel.PassengerAmount));
        }
    }
}
