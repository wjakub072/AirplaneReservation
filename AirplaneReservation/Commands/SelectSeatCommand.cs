using AirplaneReservation.ViewModels.AirplaneSeats;
using System.Windows.Media;

namespace AirplaneReservation.Commands
{
    internal sealed class SelectSeatCommand : CommandBase
    {

        private readonly AirplaneSeatCellViewModel _cellViewModel;

        public SelectSeatCommand(AirplaneSeatCellViewModel cellViewModel)
        {
            _cellViewModel = cellViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_cellViewModel.Reserved)
            {
                return;
            }

            if (_cellViewModel.Selected)
            {
                _cellViewModel.StateBrush = Brushes.White;
                _cellViewModel.Selected = false;   
            } else
            {
                _cellViewModel.StateBrush = Brushes.Blue;
                _cellViewModel.Selected = true;
            }
        }
    }
}
