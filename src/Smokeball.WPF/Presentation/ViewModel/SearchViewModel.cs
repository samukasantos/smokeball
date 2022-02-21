
using FluentValidation.Results;
using Smokeball.WPF.Presentation.ViewModel.Base;

namespace Smokeball.WPF.Presentation.ViewModel
{
    public class SearchViewModel : BaseRequestViewModel
    {
        #region Fields

        private string keyword;

        #endregion

        #region Properties

        public string Keyword
        {
            get => keyword;
            set => SetField(ref keyword, value);
        }

        #endregion

        #region Methods

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, Resources.Dictionary.MessageKeyword));
            }
            
            return ValidationResult.IsValid;
        }

        #endregion
    }
}
