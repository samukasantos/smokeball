
using Bogus;
using FluentAssertions;
using Moq;
using Smokeball.Tests.Application.Base;
using Smokeball.WPF.Application.Queries.Base;
using Smokeball.WPF.Application.Queries.Interfaces;
using Smokeball.WPF.Application.Service;
using Smokeball.WPF.Application.Service.DTO;
using Smokeball.WPF.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Smokeball.Tests.Application
{
    [Collection(nameof(ApplicationServiceCollection))]
    public class SearchServiceApplicationTests
    {
        #region Fields

        private readonly ApplicationServiceTestsFixture applicationServiceTestsFixture;

        #endregion

        #region Constructor

        public SearchServiceApplicationTests(ApplicationServiceTestsFixture applicationServiceTestsFixture)
        {
            this.applicationServiceTestsFixture = applicationServiceTestsFixture;
        }

        #endregion

        #region Methods

        [Fact(DisplayName = "Search Async")]
        [Trait("Category", "Application")]
        public async void Given_SearchGoogleAsyncIsExecuted_When_Success_Then_ShouldBeValidAndDataNotNull()
        {
            //Arrange
            var applicationService = applicationServiceTestsFixture.Mocker.CreateInstance<SearchApplicationService>();
            var searchViewModel = new SearchViewModel { Keyword = new Faker().Random.String2(10) };
            
            applicationServiceTestsFixture.Mocker.GetMock<ISearchQueries>()
                    .Setup(c => c.SearchGoogleAsync(It.IsAny<SearchDto>()))
                    .Returns(() => {

                        var result = new QueryValidationResult();
                        result.Data = new List<ResultDto>
                        {
                            new ResultDto { Title = "Item 1" },
                            new ResultDto { Title = "Item 2" }
                        };

                        return Task.FromResult(result);
                    });

            //Act
            var result = await applicationService.SearchAsync(searchViewModel);

            //Assert
            result.IsValid().Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Fact(DisplayName = "Search Async (Failed Execution)")]
        [Trait("Category", "Application")]
        public async void Given_SearchGoogleAsyncIsExecuted_When_ThrownException_Then_ReturnShouldContainErrors()
        {
            //Arrange
            var applicationService = applicationServiceTestsFixture.Mocker.CreateInstance<SearchApplicationService>();
            var searchViewModel = new SearchViewModel { Keyword = new Faker().Random.String2(10) };

            applicationServiceTestsFixture.Mocker.GetMock<ISearchQueries>()
                    .Setup(c => c.SearchGoogleAsync(It.IsAny<SearchDto>()))
                    .Throws<Exception>();

            //Act
            var result = await applicationService.SearchAsync(searchViewModel);

            //Assert
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Data.Should().BeNullOrEmpty();
        }

        #endregion
    }
}
