
using FluentValidation.Results;

namespace Smokeball.WPF.Application.Queries.Base
{
    public class QueryValidationResult : ValidationResult
    {
        #region Properties

        public object Data { get; set; }

        #endregion
    }

    public class QueryValidationResult<T> : ValidationResult
        where T : class
    {
        #region Properties

        public T Data { get; set; }

        #endregion
    }
}
