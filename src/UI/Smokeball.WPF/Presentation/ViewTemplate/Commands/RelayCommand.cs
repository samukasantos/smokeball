
using Smokeball.WPF.Presentation.ViewTemplate.Commands.Base;
using System;

namespace Smokeball.WPF.Presentation.ViewTemplate.Commands
{
    public class RelayCommand : BaseCommand
    {

        #region Fields

        private Action action;

        #endregion

        #region Constructor

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        #endregion

        #region Methods

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter) 
        {
            action?.Invoke();
        }

        #endregion
    }
}
