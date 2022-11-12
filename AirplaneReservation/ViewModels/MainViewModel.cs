using AirplaneReservation.Stores;

namespace AirplaneReservation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentVieModelChanged;
        }

        private void OnCurrentVieModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
