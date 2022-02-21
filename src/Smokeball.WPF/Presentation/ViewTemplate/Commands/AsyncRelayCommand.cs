
using Smokeball.WPF.Presentation.ViewTemplate.Commands.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Smokeball.WPF.Presentation.ViewTemplate.Commands
{
    public class AsyncRelayCommand<TViewModel> : IAsyncCommand<TViewModel>
    {
        #region Fields

        private bool isExecuting;
        private readonly Func<TViewModel, Task> execute;
        private readonly Func<TViewModel, bool> canExecute;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        public AsyncRelayCommand(Func<TViewModel, Task> execute, Func<TViewModel, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Methods

        public bool CanExecute(TViewModel parameter)
        {
            return !isExecuting && (canExecute?.Invoke(parameter) ?? true);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((TViewModel)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((TViewModel)parameter).ConfigureAwait(false);
        }

        public async Task ExecuteAsync(TViewModel parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    isExecuting = true;
                    await execute(parameter);
                }
                finally
                {
                    isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
