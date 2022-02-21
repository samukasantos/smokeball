

using Bogus;
using FluentAssertions;
using FluentValidation.Results;
using Smokeball.WPF.Application.Queries.Base;
using Smokeball.WPF.Application.Service.DTO;
using Smokeball.WPF.Presentation.ViewModel;
using Smokeball.WPF.Presentation.ViewModel.Adapter;
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
            var searchViewModel = new SearchViewModel { Keyword = new Faker().Random.String2(10) };

            //Act
            var result = searchViewModel.ToDto();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "SearchViewModel - SearchDto (Invalid)")]
        [Trait("Category", "Adapters")]
        public void Given_SearchViewModelNullInstance_When_AdapterToSearchDtoIsCalled_Then_ShoulThrowException()
        {
            //Arrange
            SearchViewModel searchViewModel = null;

            //Act
            Action act = () => searchViewModel.ToDto();

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
                new ResultDto { Title = "Item 1" },
                new ResultDto { Title = "Item 2" }
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
            var resultDto = new ResultDto { Title = new Faker().Random.String2(10) };

            //Act
            var result = resultDto.ToViewModel();

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
            Action act = () => resultDto.ToViewModel();

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
                    new ResultDto { Title = "Title 1" },
                    new ResultDto { Title = "Title 2" }
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
