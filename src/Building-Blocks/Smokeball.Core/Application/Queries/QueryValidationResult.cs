
using FluentValidation.Results;

namespace Smokeball.Core.Application.Queries
{
    public class QueryValidationResult : ValidationResult
    {
        #region Properties

        public object Data { get; set; }

        #endregion
    }
}
