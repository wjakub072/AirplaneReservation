using AirplaneReservation.ViewModels;

namespace AirplaneReservation.Commands.PassengerAmountCommands
{
    internal class LessPassengersCommand : CommandBase
    {
        private readonly PassengerAmountViewModel _viewModel;

        public LessPassengersCommand(PassengerAmountViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if(_viewModel.PassengerAmount == 1)
            {
                return;
            }

            _viewModel.PassengerAmount = _viewModel.PassengerAmount - 1;
        }
    }
}
