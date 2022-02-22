using Smokeball.Application.Service.Models;
using Smokeball.Core.Application.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Smokeball.WPF.Presentation.ViewModel.Adapter
{
    public static class ViewModelAdapter
    {
        #region Methods

        public static ResultViewModel ToViewModel(this QueryValidationResult result)
        {
            if(result == null) 
            {
                throw new ArgumentNullException(nameof(result));
            }

            var viewModel = new ResultViewModel();

            if (!result.IsValid) 
            {
                result.Errors.ForEach(e => viewModel.AddError(e.ErrorMessage));
            }
            else 
            {
                var results = result.Data as List<ResultSearchModel>;

                viewModel.Data =  results?.ToCollection();
            }

            return viewModel;
        }

        public static ResultSearchViewModel ToViewModel(this ResultSearchModel resultSearchModel) 
        {
            if(resultSearchModel == null) 
            {
                throw new ArgumentNullException(nameof(resultSearchModel));
            }

            return new ResultSearchViewModel 
            {
                Title = resultSearchModel.Title
            };
        }

        public static SearchModel ToModel(this SearchViewModel searchViewModel) 
        {
            if (searchViewModel == null)
            {
                throw new ArgumentNullException(nameof(searchViewModel));
            }

            return new SearchModel
            {
                Keyword = searchViewModel.Keyword
            };
        }

        public static ObservableCollection<ResultSearchViewModel> ToCollection(this List<ResultSearchModel> resultSearchModels)
        {
            if (resultSearchModels == null)
            {
                throw new ArgumentNullException(nameof(resultSearchModels));
            }

            var resultItems = new ObservableCollection<ResultSearchViewModel>();

            foreach (var item in resultSearchModels)
            {
                resultItems.Add(item.ToViewModel());
            }

            return resultItems;
        }

        public static SearchModel ToDto(this SearchModel searchViewModel) 
        {
            if (searchViewModel == null) 
            {
                throw new ArgumentNullException(nameof(searchViewModel));
            }

            return new SearchModel
            {
                Keyword = searchViewModel.Keyword
            };
        }

        #endregion
    }
}
