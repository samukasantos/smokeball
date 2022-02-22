
using Smokeball.Core.Models.Base;
using System.Collections.Generic;

namespace Smokeball.Application.Service.Models
{
    public class ResultModel : BaseResponseModel<List<ResultSearchModel>>
    {
        #region Properties

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        #endregion
    }
}
