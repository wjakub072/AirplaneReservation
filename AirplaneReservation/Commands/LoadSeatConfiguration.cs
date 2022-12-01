using AirplaneReservation.Providers;
using AirplaneReservation.ViewModels;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;
using System.Linq;

namespace AirplaneReservation.Commands
{
    internal class LoadSeatConfiguration : CommandBase
    {
        private readonly ReservationViewModel _viewModel;
        private readonly IDatabaseProvider _dbProvider;

        public LoadSeatConfiguration(ReservationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        //TODO
        public override async void Execute(object parameter)
        {
            // get all reservations for that flight 
            //var reservations = await _dbProvider.GetFlightReservationsAsync(_viewModel.SelectedFlight.Id);


            _viewModel.BiznesClassSeatRows = new List<AirplaneSeatRowsViewModel>();
            for (int i = 0; i < 4; i++)
            {
                AirplaneSeatRowsViewModel row = new AirplaneSeatRowsViewModel();
                int seatsInRow = 5;
                row.SeatsInRow = new AirplaneSeatCellsViewModel[seatsInRow];
                for (int j = 0; j < seatsInRow; j++)
                {
                    var seatCell = new AirplaneSeatCellsViewModel()
                    {
                        Column = j,
                        Row = i,
                        Number = $"{j}_{i}"
                    };
                    row.SeatsInRow[j] = seatCell;
                    //var reservation = reservations.Where(r => r.Seats.Any(s => s.Row == i && s.Column == j)).FirstOrDefault();
                    //if (reservation != null)
                    //{
                    //    seatCell.Reserved = true;
                    //}
                }
                _viewModel.BiznesClassSeatRows.Add(row);
            }
        }
    }
}
