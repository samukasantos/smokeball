
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Smokeball.Application.Queries;
using Smokeball.Application.Service.Configuration;
using Smokeball.Application.Service.DTO;
using Smokeball.Infra.Http.Interfaces;
using Smokeball.Tests.Queries.Base;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Smokeball.Tests.Queries
{
    [Collection(nameof(QueriesCollection))]
    public class SearchQueriesTests
    {
        #region Fields

        private readonly QueriesTestsFixture queriesTestsFixture;

        #endregion

        #region Constructor

        public SearchQueriesTests(QueriesTestsFixture queriesTestsFixture)
        {
            this.queriesTestsFixture = queriesTestsFixture;
        }

        #endregion

        #region Methods

        [Fact(DisplayName = "Search Google Async")]
        [Trait("Category", "Queries")]
        public async void Given_SearchGoogleAsyncIsExecuted_When_Success_Then_ShouldReturnValidData()
        {
            //Arrange
            var query = "conveyancing,software";

            SetupMocker();

            var searchQueries = queriesTestsFixture.Mocker.CreateInstance<SearchQueries>();

            queriesTestsFixture.Mocker.GetMock<IHttpConnector>()
                .Setup(c => c.GetListAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult(queriesTestsFixture.CreateHmtlContent());
                });

            //Act
            var result = await searchQueries.SearchGoogleAsync(new SearchDto { Keyword = query });

            //Assert
            result.IsValid.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }


        [Fact(DisplayName = "Search Google Async (No response)")]
        [Trait("Category", "Queries")]
        public async void Given_SearchGoogleAsyncIsExecuted_When_SuccessButNoResponse_Then_DataShouldBeNull()
        {
            //Arrange
            var query = "conveyancing,software";

            SetupMocker();

            var searchQueries = queriesTestsFixture.Mocker.CreateInstance<SearchQueries>();

            queriesTestsFixture.Mocker.GetMock<IHttpConnector>()
                .Setup(c => c.GetListAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult(string.Empty);
                });

            //Act
            var result = await searchQueries.SearchGoogleAsync(new SearchDto { Keyword = query });

            //Assert
            result.Data.Should().BeNull();
        }

        [Fact(DisplayName = "Search Google Async (Exception)")]
        [Trait("Category", "Queries")]
        public async void Given_SearchGoogleAsyncIsExecuted_When_ThrowException_ShoudContainErrors()
        {
            //Arrange
            var query = "conveyancing,software";

            SetupMocker();

            var searchQueries = queriesTestsFixture.Mocker.CreateInstance<SearchQueries>();

            queriesTestsFixture.Mocker.GetMock<IHttpConnector>()
                .Setup(c => c.GetListAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("Internal Server Error"));

            //Act
            var result = await searchQueries.SearchGoogleAsync(new SearchDto { Keyword = query });

            //Assert
            result.Data.Should().BeNull();
            result.Errors.Should().HaveCountGreaterThan(0);
        }

        private void SetupMocker()
        {
            queriesTestsFixture.Mocker.GetMock<IHttpConnector>()
                    .Setup(c => c.BaseUri)
                    .Returns("https://www.google.com.au/");

            queriesTestsFixture.Mocker.GetMock<IOptions<GoogleHttpIntegration>>()
                .Setup(c => c.Value)
                .Returns(new GoogleHttpIntegration
                {
                    BaseUri = "https://www.google.com.au/",
                    Search = "search",
                    SearchOcurrency = 100,
                    SearchRole = "heading"
                });

            queriesTestsFixture.Mocker.GetMock<IOptions<GoogleRegexPatterns>>()
                .Setup(c => c.Value)
                .Returns(new GoogleRegexPatterns { GoogleH3Pattern = "<h3 class=\".*?\"><div class=\".*?\">([^<]+)</div></h3>" });
        }

        #endregion
    }
}
