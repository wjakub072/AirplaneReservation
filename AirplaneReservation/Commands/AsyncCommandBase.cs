using System;
using System.Threading.Tasks;
using System.Windows;

namespace AirplaneReservation.Commands
{
    internal abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;

        private bool isExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                OnCanExecutedChanged();
            }
        }


        public override bool CanExecute(object parameter)
        {
            return !isExecuting && base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            isExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n" + ex.InnerException.Message;
                }
                MessageBox.Show(message);
            }
            finally
            {
                isExecuting = false;
            }

        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
