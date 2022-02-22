
using Smokeball.Application.Service.DTO;
using Smokeball.Application.Service.Models;
using Smokeball.Core.Application.Queries;
using System;
using System.Collections.Generic;

namespace Smokeball.Application.Adapter
{
    public static class ApplicationAdapter
    {
        #region Methods
        
        public static SearchDto ToDto(this SearchModel searchModel)
        {
            if (searchModel == null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            return new SearchDto
            {
                Keyword = searchModel.Keyword
            };
        }

        public static ResultModel ToViewModel(this QueryValidationResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var model = new ResultModel();

            if (!result.IsValid)
            {
                result.Errors.ForEach(e => model.AddError(e.ErrorMessage));
            }
            else
            {
                var results = result.Data as List<ResultDto>;

                model.Data = results?.ToCollection();
            }

            return model;
        }

        public static List<ResultSearchModel> ToCollection(this List<ResultDto> resultDtoCollection)
        {
            if (resultDtoCollection == null)
            {
                throw new ArgumentNullException(nameof(resultDtoCollection));
            }

            var resultItems = new List<ResultSearchModel>();

            foreach (var item in resultDtoCollection)
            {
                resultItems.Add(item.ToModel());
            }

            return resultItems;
        }

        public static ResultSearchModel ToModel(this ResultDto resultDto)
        {
            if (resultDto == null)
            {
                throw new ArgumentNullException(nameof(resultDto));
            }

            return new ResultSearchModel
            {
                Title = resultDto.Value
            };
        }

        #endregion
    }
}
