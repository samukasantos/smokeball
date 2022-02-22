
using Smokeball.Application.Adapter;
using Smokeball.Application.Queries.Interfaces;
using Smokeball.Application.Service.Interfaces;
using Smokeball.Application.Service.Models;
using Smokeball.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Smokeball.Application.Service
{
    public class SearchApplicationService : ISearchApplicationService
    {
        #region Fields

        private readonly ISearchQueries queries;

        #endregion

        #region Constructor 

        public SearchApplicationService(ISearchQueries queries)
        {
            this.queries = queries;
        }

        #endregion

        #region Methods

        public async Task<ResultModel> SearchAsync(SearchModel searchModel)
        {
            try
            {
                var result = await queries.SearchGoogleAsync(searchModel.ToDto());

                return result.ToViewModel();
            }
            catch (Exception e)
            {
                var resultModel = new ResultModel();

                resultModel.AddError(e.GetAllMessages());

                return resultModel;
            }
        }

        #endregion
    }
}
