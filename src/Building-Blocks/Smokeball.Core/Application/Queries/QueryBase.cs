
using FluentValidation.Results;

namespace Smokeball.Core.Application.Queries
{
    public abstract class QueryBase
    {
        #region Fields

        protected readonly QueryValidationResult ValidationResult = new QueryValidationResult();

        #endregion

        #region Methods

        protected void CreateResponse(object result)
        {
            ValidationResult.Data = result;
        }

        protected void AddError(string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
        }

        #endregion
    }
}
