using AirplaneReservation.Commands;
using AirplaneReservation.Services.Interfaces;
using System.Windows.Input;

namespace AirplaneReservation.ViewModels
{
    internal class ConfirmationViewModel : ViewModelBase
    {
        public ICommand TimetableNavigationCommand { get; }

        public ConfirmationViewModel(INavigationService timetableNavigationService)
        {
            TimetableNavigationCommand = new TimetableNavigationCommand(timetableNavigationService);
        }
    }
}
