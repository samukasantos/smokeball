

using FluentAssertions;
using Smokeball.Infra.Http;
using Smokeball.Tests.Services.Base;
using System;
using Xunit;

namespace Smokeball.Tests.Services
{
    [Collection(nameof(ServicesCollection))]
    public class HttpConnectorTests
    {
        #region Fields

        private readonly ServicesTestsFixture serviceTestsFixture;

        #endregion

        #region Constructor

        public HttpConnectorTests(ServicesTestsFixture serviceTestsFixture)
        {
            this.serviceTestsFixture = serviceTestsFixture;
        }

        #endregion

        #region Methods


        [Fact(DisplayName = "HttpConnector MaxAttempts (Less Than 0)")]
        [Trait("Category", "Services")]
        public void Given_MaxAttempts_When_LessThenZero_Then_ShouldKeepTheDefaultValue()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
            var defaultMaxAttempts = 3;

            //Act
            httpConnector.SetMaxRetryAttempts(-1);

            //Assert
            httpConnector.MaxRetryAttempts.Should().Be(defaultMaxAttempts);
        }

        [Fact(DisplayName = "HttpConnector MaxAttempts (Equal 0)")]
        [Trait("Category", "Services")]
        public void Given_MaxAttempts_When_EqualZero_Then_ShouldKeepTheDefaultValue()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
            var defaultMaxAttempts = 3;

            //Act
            httpConnector.SetMaxRetryAttempts(0);

            //Assert
            httpConnector.MaxRetryAttempts.Should().Be(defaultMaxAttempts);
        }

        [Fact(DisplayName = "HttpConnector MaxAttempts (More Than Default)")]
        [Trait("Category", "Services")]
        public void Given_MaxAttempts_When_MoreThanDefault_Then_ShouldMaxAttemptsReturnTheNewValue()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
            var maxAttempts = 6;

            //Act
            httpConnector.SetMaxRetryAttempts(maxAttempts);

            //Assert
            httpConnector.MaxRetryAttempts.Should().Be(maxAttempts);
        }

        [Fact(DisplayName = "HttpConnector MaxAttempts (Comparing Default)")]
        [Trait("Category", "Services")]
        public void Given_MaxAttempts_When_Initilized_Then_ShouldMaxAttemptsReturnTheDefautlValue()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
            var defaultMaxAttempts = 3;

            //Act

            //Assert
            httpConnector.MaxRetryAttempts.Should().Be(defaultMaxAttempts);
        }


        [Fact(DisplayName = "HttpConnector Null ResourceUri ")]
        [Trait("Category", "Services")]
        public void Given_HttpConnector_When_GetListAsyncIsCalledWithNullOrEmptyResourceUri_Then_ShouldThrownArgumentNullException()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();

            //Act
            Action act = () => _= httpConnector.GetListAsync(string.Empty, "query").Result;

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        //[Fact(DisplayName = "HttpConnector Null ResourceUri ")]
        //[Trait("Category", "Services")]
        //public void Given_HttpConnector_When_GetListAsyncIsCalledWithResourceUri_Then_ShouldBuildAValidUri()
        //{
        //    //Arrange
        //    var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();

        //    //Act
        //    Action act = () => _ = httpConnector.GetListAsync("search", "page=1&total=100").Result;

        //    //Assert
        //    act.Should().NotThrow<ArgumentNullException>();
        //}

        #endregion
    }
}
