
using FluentValidation.Results;

namespace Smokeball.WPF.Application.Queries.Base
{
    public abstract class QueryBase
    {
        #region Fields

        protected readonly QueryValidationResult ValidationResult = new QueryValidationResult();

        #endregion

        #region Methods

        protected QueryValidationResult CreateResponse(object result)
        {
            ValidationResult.Data = result;

            return ValidationResult;
        }

        protected void AddError(string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
        }

        #endregion


    }
}
