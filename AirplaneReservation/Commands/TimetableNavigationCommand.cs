using AirplaneReservation.Services.Interfaces;

namespace AirplaneReservation.Commands
{
    internal class TimetableNavigationCommand : CommandBase
    {
        private readonly INavigationService _timetableNavigationService;

        public TimetableNavigationCommand(INavigationService timetableNavigationService)
        {
            _timetableNavigationService = timetableNavigationService;
        }

        public override void Execute(object parameter)
        {
            _timetableNavigationService.Navigate();
        }
    }
}
