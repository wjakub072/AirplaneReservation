using AirplaneReservation.Services;
using AirplaneReservation.ViewModels;

namespace AirplaneReservation.Commands
{
    internal class PassengerAmountNavigationCommand : CommandBase
    {
        private readonly TimetableViewModel _timeTableViewModel;
        private readonly IParameterNavigationService _navigationService;

        public PassengerAmountNavigationCommand(TimetableViewModel timeTableViewModel, IParameterNavigationService navigationService)
        {
            _timeTableViewModel = timeTableViewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            // navigate with selected flight passed as parameter
            _navigationService.Navigate(_timeTableViewModel.SelectedFlight);
        }
    }
}
