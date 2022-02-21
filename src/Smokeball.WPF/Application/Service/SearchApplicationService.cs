
using Smokeball.WPF.Application.Queries.Interfaces;
using Smokeball.WPF.Application.Service.Interfaces;
using Smokeball.WPF.Extensions;
using Smokeball.WPF.Presentation.ViewModel;
using Smokeball.WPF.Presentation.ViewModel.Adapter;
using Smokeball.WPF.Presentation.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smokeball.WPF.Application.Service
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

        public async Task<ResultViewModel> SearchAsync(SearchViewModel searchViewModel)
        {
            try
            {
                var result = await queries.SearchGoogleAsync(searchViewModel.ToDto());

                return result.ToViewModel();
            }
            catch (Exception e)
            {
                var resultViewModel = new ResultViewModel();

                resultViewModel.AddError(e.GetAllMessages());

                return resultViewModel;
            }
        }

        #endregion
    }
}
