
using Smokeball.WPF.Application.Queries.Base;
using Smokeball.WPF.Application.Service.DTO;
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
                var results = result.Data as List<ResultDto>;

                viewModel.Data =  results?.ToCollection();
            }

            return viewModel;
        }

        public static ResultSearchViewModel ToViewModel(this ResultDto resultDto) 
        {
            if(resultDto == null) 
            {
                throw new ArgumentNullException(nameof(resultDto));
            }

            return new ResultSearchViewModel 
            {
                Title = resultDto.Title
            };
        }

        public static ObservableCollection<ResultSearchViewModel> ToCollection(this List<ResultDto> resultDto)
        {
            if (resultDto == null)
            {
                throw new ArgumentNullException(nameof(resultDto));
            }

            var resultItems = new ObservableCollection<ResultSearchViewModel>();

            foreach (var item in resultDto)
            {
                resultItems.Add(item.ToViewModel());
            }

            return resultItems;
        }

        public static SearchDto ToDto(this SearchViewModel searchViewModel) 
        {
            if (searchViewModel == null) 
            {
                throw new ArgumentNullException(nameof(searchViewModel));
            }

            return new SearchDto
            {
                Keyword = searchViewModel.Keyword
            };
        }

        #endregion
    }
}
