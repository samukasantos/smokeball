

using FluentAssertions;
using Smokeball.Infra.Http;
using Smokeball.Tests.Services.Base;
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

        //[Fact(DisplayName = "HttpConnector MaxAttempts (Equal 0)")]
        //[Trait("Category", "Services")]
        //public void Given_MaxAttempts_When_EqualZero_Then_ShouldKeepTheDefaultValue()
        //{
        //    //Arrange
        //    var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
        //    var defaultMaxAttempts = 3;

        //    //Act
        //    httpConnector.SetMaxRetryAttempts(0);

        //    //Assert
        //    httpConnector.MaxRetryAttempts.Should().Be(defaultMaxAttempts);
        //}

        #endregion
    }
}
