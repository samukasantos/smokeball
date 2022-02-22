

using Bogus;
using FluentAssertions;
using FluentValidation.Results;
using Smokeball.Application.Adapter;
using Smokeball.Application.Service.DTO;
using Smokeball.Application.Service.Models;
using Smokeball.Core.Application.Queries;
using Smokeball.WPF.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Smokeball.Tests.Adapter
{
    public class AdapterTests
    {
        [Fact(DisplayName = "SearchViewModel - SearchDto (Valid)")]
        [Trait("Category", "Adapters")]
        public void Given_SearchViewModelInstance_When_AdapterToSearchDtoIsCalled_Then_ShoulReturnValidViewModelInstance()
        {
            //Arrange
            var searchModel = new SearchModel { Keyword = new Faker().Random.String2(10) };

            //Act
            var result = searchModel.ToDto();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "SearchViewModel - SearchDto (Invalid)")]
        [Trait("Category", "Adapters")]
        public void Given_SearchViewModelNullInstance_When_AdapterToSearchDtoIsCalled_Then_ShoulThrowException()
        {
            //Arrange
            SearchModel searchModel = null;

            //Act
            Action act = () => searchModel.ToDto();

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "List<ResultDto> - ObservableCollection<ResultSearchViewModel> (Valid)")]
        [Trait("Category", "Adapters")]
        public void Given_ListResultDtoInstance_When_AdapterToObservableCollectionResultSearchViewModelIsCalled_Then_ShoulReturnListInstance()
        {
            //Arrange
            var list = new List<ResultDto>
            {
                new ResultDto { Value = "Item 1" },
                new ResultDto { Value = "Item 2" }
            };

            //Act
            var result = list.ToCollection();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "List<ResultDto> - ObservableCollection<ResultSearchViewModel> (Invalid)")]
        [Trait("Category", "Adapters")]
        public void Given_ListResultDtoNullInstance_When_AdapterToObservableCollectionResultSearchViewModelIsCalled_Then__ShoulThrowException()
        {
            //Arrange
            List<ResultDto> items = null;

            //Act
            Action act = () => items.ToCollection();

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "ResultDto - ResultSearchViewModel (Valid)")]
        [Trait("Category", "Adapters")]
        public void Given_ResultDtoInstance_When_AdapterToResultSearchViewModelIsCalled_Then_ShoulReturnValidViewModelInstance()
        {
            //Arrange
            var resultDto = new ResultDto { Value = new Faker().Random.String2(10) };

            //Act
            var result = resultDto.ToModel();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "ResultDto - ResultSearchViewModel (Invalid)")]
        [Trait("Category", "Adapters")]
        public void Given_ResultDtoNullInstance_When_AdapterToResultSearchViewModelIsCalled_Then_ShoulThrowException()
        {
            //Arrange
            ResultDto resultDto = null;

            //Act
            Action act = () => resultDto.ToModel();

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }


        [Fact(DisplayName = "QueryValidationResult - ResultViewModel (Valid)")]
        [Trait("Category", "Adapters")]
        public void Given_QueryValidationResultInstance_When_AdapterToResultViewModelIsCalled_Then_ShoulReturnValidViewModelInstance()
        {
            //Arrange
            var queryValidationResult = new QueryValidationResult
            {
                Data = new List<ResultDto>
                {
                    new ResultDto { Value = "Title 1" },
                    new ResultDto { Value = "Title 2" }
                }
            };
            //Act
            var result = queryValidationResult.ToViewModel();

            //Assert
            result.IsValid().Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "QueryValidationResult - ResultViewModel (Invalid)")]
        [Trait("Category", "Adapters")]
        public void Given_QueryValidationResultNullInstance_When_AdapterToResultViewModelIsCalled_Then_ShoulThrowException()
        {
            //Arrange
            QueryValidationResult queryValidationResult = null;

            //Act
            Action act = () => queryValidationResult.ToViewModel();

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }


        [Fact(DisplayName = "QueryValidationResult - ResultViewModel (Errors)")]
        [Trait("Category", "Adapters")]
        public void Given_QueryValidationResultInstanceHasErrors_When_AdapterToResultViewModelIsCalled_Then_ShoulErrorBeMoreThanZero()
        {
            //Arrange
            var queryValidationResult = new QueryValidationResult();
            queryValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Failed"));

            //Act
            var result = queryValidationResult.ToViewModel();

            //Assert
            result.IsValid().Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
        }
    }
}
