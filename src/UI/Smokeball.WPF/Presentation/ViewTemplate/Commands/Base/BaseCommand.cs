

using System;
using System.Windows.Input;

namespace Smokeball.WPF.Presentation.ViewTemplate.Commands.Base
{
    public abstract class BaseCommand : ICommand
    {
        #region Events

        public event EventHandler CanExecuteChanged;
        public virtual bool CanExecute(object parameter) => true;

        #endregion

        #region Methods

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
