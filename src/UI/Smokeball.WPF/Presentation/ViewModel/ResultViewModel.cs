
using Smokeball.WPF.Presentation.ViewModel.Base;
using System.Collections.ObjectModel;

namespace Smokeball.WPF.Presentation.ViewModel
{
    public class ResultViewModel : BaseResponseViewModel<ObservableCollection<ResultSearchViewModel>>
    {
        #region Properties

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        #endregion
    }
}
