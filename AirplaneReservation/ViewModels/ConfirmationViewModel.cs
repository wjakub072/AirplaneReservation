using AirplaneReservation.Commands;
using System.Windows.Input;

namespace AirplaneReservation.ViewModels
{
    internal class ConfirmationViewModel : ViewModelBase
    {
        public ICommand TimetableNavigationCommand { get; }

        public ConfirmationViewModel(Services.INavigationService timetableNavigationService)
        {
            TimetableNavigationCommand = new TimetableNavigationCommand(timetableNavigationService);
        }
    }
}
