using AirplaneReservation.Factories;
using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.ViewModels;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AirplaneReservation.Commands
{
    internal class ConfirmNavigationCommand : AsyncCommandBase
    {
        private readonly INavigationService _confirmationNavigationService;
        private readonly ReservationViewModel _viewModel;
        private readonly IReservationFactory _reservationFactory;
        private readonly IDatabaseAccessService _databaseAccessService;

        public ConfirmNavigationCommand(INavigationService confirmationNavigationService, IReservationFactory reservationFactory, IDatabaseAccessService databaseAccessService, ReservationViewModel viewModel)
        {
            _confirmationNavigationService = confirmationNavigationService;
            _reservationFactory = reservationFactory;
            _databaseAccessService = databaseAccessService;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedSeats = new List<AirplaneSeatCellViewModel>();
            foreach (var biznesRow in _viewModel.BiznesClassSeatRows)
            {
                selectedSeats.AddRange(biznesRow.SeatsInRow.Where(c => c.Selected == true).ToList());
            }
            foreach (var economicRow in _viewModel.EconomicClassSeatRows)
            {
                selectedSeats.AddRange(economicRow.SeatsInRow.Where(c => c.Selected == true).ToList());
            }

            if (selectedSeats.Count != _viewModel.AmountOfPassengers)
            {
                MessageBox.Show($"Wybrano niezgodną ilość miejsc ({selectedSeats.Count}) względem uprzednio zadeklarowanej ({_viewModel.AmountOfPassengers}).", 
                    "Błąd", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            var newReservation = _reservationFactory.CreateReservation(selectedSeats, _viewModel.SelectedFlight.Id);

            await _databaseAccessService.AddReservation(newReservation);

            _confirmationNavigationService.Navigate();
        }
    }
}
