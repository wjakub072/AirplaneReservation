using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.Stores;
using AirplaneReservation.ViewModels;
using System;

namespace AirplaneReservation.Services
{

    public class ParameterNavigationService : IParameterNavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<object, ViewModelBase> _createViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<object, ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}
