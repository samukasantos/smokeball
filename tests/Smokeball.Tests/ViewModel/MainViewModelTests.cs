
using Bogus;
using FluentAssertions;
using Moq;
using Smokeball.Application.Service.Interfaces;
using Smokeball.Application.Service.Models;
using Smokeball.Tests.ViewModel.Base;
using Smokeball.WPF.Presentation.ViewModel;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Smokeball.Tests.ViewModel
{
    [Collection(nameof(ViewModelCollection))]
    public class MainViewModelTests
    {
        #region Fields

        private readonly ApplicationServiceTestsFixture viewModelTestsFixture;
        private readonly ITestOutputHelper outputHelper;

        #endregion

        #region Constructor

        public MainViewModelTests(ApplicationServiceTestsFixture viewModelTestsFixture, ITestOutputHelper outputHelper)
        {
            this.viewModelTestsFixture = viewModelTestsFixture;
            this.outputHelper = outputHelper;
        }

        #endregion

        #region Methods

        [Fact(DisplayName = "Invalid Search Criteria")]
        [Trait("Category", "ViewModel")]
        public void Given_SearchCriteriaIsCreated_When_EmptyOrNull_Then_ErrorIsRaised()
        {
            //Arrange
            var searchViewModel = viewModelTestsFixture.GenerateInvalidSearchViewModel();

            //Act
            var result = searchViewModel.IsValid();

            //Assert
            result.Should().BeFalse();
            searchViewModel.Errors.Should().HaveCountGreaterThan(0);

            outputHelper.WriteLine($"Found {searchViewModel.Errors.Count} error(s).");
        }

        [Fact(DisplayName = "Invalid Search Criteria Showing Errors")]
        [Trait("Category", "ViewModel")]
        public void Given_SearchCriteriaIsCreated_When_EmptyOrNull_Then_ShouldDisplayErrors()
        {
            //Arrange
            var mainViewModel = viewModelTestsFixture.Mocker.CreateInstance<MainViewModel>();
            var searchViewModel = viewModelTestsFixture.GenerateInvalidSearchViewModel();

            //Act
            mainViewModel.SearchCommand.Execute(searchViewModel);

            //Assert
            mainViewModel.HasError.Should().BeTrue();
            mainViewModel.ErrorMessage.Should().NotBeNullOrEmpty();
            searchViewModel.Errors.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Valid Search Criteria")]
        [Trait("Category", "ViewModel")]
        public void Given_SearchCriteriaIsCreated_When_IsNotEmptyOrNull_Then_ErrorIsRaised()
        {
            //Arrange
            var searchViewModel = viewModelTestsFixture.GenerateValidSearchViewModel();

            //Act
            var result = searchViewModel.IsValid();

            //Assert
            result.Should().BeTrue();
            searchViewModel.Errors.Should().HaveCount(0);

            outputHelper.WriteLine($"Found {searchViewModel.Errors.Count} error(s).");
        }

        [Fact(DisplayName = "Search Command (IsBusy)")]
        [Trait("Category", "ViewModel")]
        public void Given_IsBusyHasTrueValue_When_CommandIsChecked_Then_CommandCanNotBeExecuted()
        {
            //Arrange
            var mainViewModel = viewModelTestsFixture.Mocker.CreateInstance<MainViewModel>();
            var searchViewModel = viewModelTestsFixture.GenerateInvalidSearchViewModel();

            //Act
            mainViewModel.IsBusy = true;
            var result = mainViewModel.SearchCommand.CanExecute(searchViewModel);

            //Assert
            result.Should().BeFalse();

            outputHelper.WriteLine($"Test Command.");
        }

        [Fact(DisplayName = "Search Command (ResultViewModel with Errors)")]
        [Trait("Category", "ViewModel")]
        public void Given_SearchAsyncIsExecuted_When_IsNotValid_Then_ShouldHaveErrors()
        {
            //Arrange
            var mainViewModel = viewModelTestsFixture.Mocker.CreateInstance<MainViewModel>();
            var searchViewModel = viewModelTestsFixture.GenerateValidSearchViewModel();

            viewModelTestsFixture.Mocker.GetMock<ISearchApplicationService>()
                    .Setup(c => c.SearchAsync(It.IsAny<SearchModel>()))
                    .Returns(() => {

                        var result = new ResultModel();
                        result.AddError("Error Message");

                        return Task.FromResult(result);
                    });

            //Act
            mainViewModel.SearchCommand.Execute(searchViewModel);

            //Assert
            mainViewModel.HasError.Should().BeTrue();
            mainViewModel.ErrorMessage.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "Close Command")]
        [Trait("Category", "ViewModel")]
        public void Given_CloseCommand_When_IsExecuted_Then_ErrorAndMessageShouldBeReseted()
        {
            //Arrange
            var mainViewModel = viewModelTestsFixture.Mocker.CreateInstance<MainViewModel>();

            //Act
            mainViewModel.CloseCommand.Execute(null);

            //Assert
            mainViewModel.IsBusy.Should().BeFalse();
            mainViewModel.ErrorMessage.Should().BeNullOrEmpty();
        }


        #endregion
    }
}
