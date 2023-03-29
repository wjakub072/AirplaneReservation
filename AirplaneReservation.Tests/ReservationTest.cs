using AirplaneReservation.Exceptions;
using AirplaneReservation.ViewModels;

namespace AirplaneReservation.Tests
{
    public class ReservationTest : IClassFixture<ReservationViewModel>
    {
        private readonly ReservationViewModel _viewModel;

        public ReservationTest(ReservationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        [Theory]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        [InlineData(5, 4)]
        public void InvalidAmountOfSelectedSeatsShouldThrow(int amountOfPassengers, int selectedSealts)
        {
            try
            {
                _viewModel.AmountOfPassengers = amountOfPassengers;
                for (int i = 0; i < selectedSealts; i++)
                {
                    _viewModel.EconomicClassSeatRows[0].SeatsInRow[i].Selected = true;
                }

                _viewModel.ConfirmNavigationCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Assert.IsType<ReservationException>(ex);
            }
        }
    }
}