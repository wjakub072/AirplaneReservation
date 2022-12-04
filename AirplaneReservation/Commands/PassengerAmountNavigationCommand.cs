using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.ViewModels;

namespace AirplaneReservation.Commands
{
    internal sealed class PassengerAmountNavigationCommand : CommandBase
    {
        private readonly TimetableViewModel _timeTableViewModel;
        private readonly IParameterNavigationService _passengerAmountNavigationService;

        public PassengerAmountNavigationCommand(TimetableViewModel timeTableViewModel, 
            IParameterNavigationService passengerAmountNavigationService)
        {
            _timeTableViewModel = timeTableViewModel;
            _passengerAmountNavigationService = passengerAmountNavigationService;
        }

        public override void Execute(object parameter)
        {
            // navigate with selected flight passed as parameter
            _passengerAmountNavigationService.Navigate(_timeTableViewModel.SelectedFlight);
        }
    }
}
