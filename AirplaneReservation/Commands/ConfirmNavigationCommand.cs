using AirplaneReservation.Exceptions;
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
            try
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
                    throw new ReservationException(_viewModel.AmountOfPassengers, selectedSeats.Count);
                }

                var newReservation = _reservationFactory.CreateReservation(selectedSeats, _viewModel.SelectedFlight.Id);

                await _databaseAccessService.AddReservation(newReservation);

                _confirmationNavigationService.Navigate();
            }
            catch (ReservationException rExt)
            {
                MessageBox.Show(rExt.Message,
                        "Błąd",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            catch (System.Exception)
            {
                // EXCEPTION OF OTHER TYPE THAN RESERVATIONEXCEPTION ARE NOT PLANNED, 
                // SO WE FILTER THEM WITH ABOVE STATEMENT 
                // TODO: LOG TO DATABASE 
                throw;
            }
        }
    }
}
