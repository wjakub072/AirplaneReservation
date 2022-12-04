using AirplaneReservation.Models;
using AirplaneReservation.Providers;
using AirplaneReservation.ViewModels;
using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AirplaneReservation.Commands
{
    internal sealed class LoadSeatConfiguration : AsyncCommandBase
    {
        private readonly IDatabaseProvider _dbProvider;
        private readonly ReservationViewModel _viewModel;
        private readonly IDictionary<int, char> _rowCharDictionary;

        public LoadSeatConfiguration(IDatabaseProvider dbProvider, ReservationViewModel viewModel)
        {
            _dbProvider = dbProvider;
            _viewModel = viewModel;
            _rowCharDictionary = new Dictionary<int, char>()
            {
                { 0, 'A' },
                { 1, 'B' },
                { 2, 'C' },
                { 3, 'D' }
            };
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var reservations = await _dbProvider.GetFlightReservationsAsync(_viewModel.SelectedFlight.Id);

            loadBiznesSeats(_viewModel.SelectedFlight, 
                reservations.Where(reservation => reservation.Seats.Any(seat => seat.Biznes == true)));

            loadEconomicSeats(_viewModel.SelectedFlight,
                reservations.Where(reservation => reservation.Seats.Any(seat => seat.Biznes == false)));
        }

        /// <summary>
        /// Method loads biznes class seat cells to viewModel's BiznesClassSeatRows list. Numbers of seats, are based on Flight DTO properties.
        /// </summary>
        /// <param name="flightPlane">Flight that seats are going to be displayed.</param>
        /// <param name="flightReservations">Reservations that has been made for biznes class seats.</param>
        private void loadBiznesSeats(Flight flightPlane, IEnumerable<Reservation> flightReservations)
        {
            _viewModel.BiznesClassSeatRows = new List<AirplaneSeatRowViewModel>();
            for (int i = 0; i < flightPlane.BiznesRowsInPlane; i++)
            {
                AirplaneSeatRowViewModel row = new AirplaneSeatRowViewModel();
                row.SeatsInRow = new AirplaneSeatCellViewModel[flightPlane.BiznesSeatsInRow];
                for (int j = 0; j < flightPlane.BiznesSeatsInRow; j++)
                {
                    var seatCell = new AirplaneSeatCellViewModel()
                    {
                        Column = j,
                        Row = i,
                        Selected = false,
                        StateBrush = Brushes.White
                    };
                    seatCell.GenerateSeatNumber(_rowCharDictionary, i, j);

                    var reservation = flightReservations.Where(res => 
                        res.Seats.Any(seat => seat.Row == seatCell.Row && seat.Column == seatCell.Column))
                        .FirstOrDefault();
                    if (reservation != null)
                    {
                        seatCell.Reserved = true;
                        seatCell.StateBrush = Brushes.Red;
                    }

                    row.SeatsInRow[j] = seatCell;
                }
                _viewModel.BiznesClassSeatRows.Add(row);
            }
        }

        private void loadEconomicSeats(Flight flightPlane, IEnumerable<Reservation> flightReservations)
        {
            _viewModel.EconomicClassSeatRows = new List<AirplaneSeatRowViewModel>();
            for (int i = 0; i < flightPlane.EconomicRowsInPlane; i++)
            {
                AirplaneSeatRowViewModel row = new AirplaneSeatRowViewModel();
                row.SeatsInRow = new AirplaneSeatCellViewModel[flightPlane.EconomicSeatsInRow];
                for (int j = 0; j < flightPlane.EconomicSeatsInRow; j++)
                {
                    var seatCell = new AirplaneSeatCellViewModel()
                    {
                        Column = j,
                        Row = i,
                        Selected = false,
                        StateBrush = Brushes.White
                    };
                    seatCell.GenerateSeatNumber(_rowCharDictionary, i, j);

                    var reservation = flightReservations.Where(res => 
                    res.Seats.Any(seat => seat.Row == seatCell.Row && seat.Column == seatCell.Column))
                        .FirstOrDefault();
                    if (reservation != null)
                    {
                        seatCell.Reserved = true;
                        seatCell.StateBrush = Brushes.Red;
                    }

                    row.SeatsInRow[j] = seatCell;
                }
                _viewModel.BiznesClassSeatRows.Add(row);
            }
        }
    }
}
