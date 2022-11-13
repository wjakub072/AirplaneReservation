using AirplaneReservation.Services;

namespace AirplaneReservation.Commands
{
    internal class ConfirmNavigationCommand : CommandBase
    {
        private readonly INavigationService _confirmationNavigationService;

        public ConfirmNavigationCommand(INavigationService confirmationNavigationService)
        {
            _confirmationNavigationService = confirmationNavigationService;
        }

        public override void Execute(object parameter)
        {
            _confirmationNavigationService.Navigate();
        }
    }
}
