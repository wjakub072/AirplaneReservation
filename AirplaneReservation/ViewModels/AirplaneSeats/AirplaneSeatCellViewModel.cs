using AirplaneReservation.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace AirplaneReservation.ViewModels.AirplaneSeats
{
    internal class AirplaneSeatCellViewModel : ViewModelBase
    {
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set 
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private Brush _stateBrush;
        public Brush StateBrush
        {
            get { return _stateBrush; }
            set 
            { 
                _stateBrush = value;
                OnPropertyChanged(nameof(StateBrush));
            }
        } 

        public bool Reserved { get; set; }
        public string Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        /// <summary>
        /// Generate and assign seats' number based on passed parameters.
        /// </summary>
        /// <param name="rowCharDictionary">Dictionary that holds map for row letters</param>
        /// <param name="row">Number assigned to row that seat is placed in</param>
        /// <param name="column">Number assigned to column that seat is placed in</param>
        public void GenerateSeatNumber(IDictionary<int, char> rowCharDictionary, int row, int column)
        {
            if (column < 9)
            {
                Number = $"{rowCharDictionary[row]}0{column}";
            }
            else
            {
                Number = $"{rowCharDictionary[row]}{column}";
            }
        }

        public ICommand SelectSeatCommand { get; set; }
        public AirplaneSeatCellViewModel()
        {
            SelectSeatCommand = new SelectSeatCommand(this);
        }
    }
}
