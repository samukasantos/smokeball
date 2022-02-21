

using FluentValidation.Results;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Smokeball.WPF.Presentation.ViewModel.Base
{
    public abstract class BaseRequestViewModel : BaseViewModel
    {
        #region Fields

        protected ValidationResult ValidationResult = new ValidationResult();

        #endregion

        #region Properties

        public IReadOnlyCollection<string> Errors 
            => new ReadOnlyCollection<string>(ValidationResult.Errors.Select(v => v.ErrorMessage).ToList());

        #endregion

        #region Methods

        public abstract bool IsValid();

        #endregion
    }
}
