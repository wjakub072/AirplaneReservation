using AirplaneReservation.ViewModels;

namespace AirplaneReservation.Commands.PassengerAmountCommands
{
    internal class MorePassengersCommand : CommandBase
    {
        private readonly PassengerAmountViewModel _viewModel;

        public MorePassengersCommand(PassengerAmountViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.PassengerAmount++;
        }
    }
}
