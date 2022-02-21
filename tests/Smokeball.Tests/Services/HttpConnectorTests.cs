

using FluentAssertions;
using Moq;
using Smokeball.Tests.Services.Base;
using Smokeball.WPF.Infra.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [Fact(DisplayName = "HttpConnector", Skip = "Review")]
        [Trait("Category", "Services")]
        public async void Given_HttpConnectorGetListAsyncIsCalled_When_Success_Then_ShouldReturnValidResponse()
        {
            //Arrange
            var httpConnector = serviceTestsFixture.Mocker.CreateInstance<HttpConnector>();
            httpConnector.BaseUri = "https://www.google.com.au/";
            var resourceUri = "search";
            var query = "conveyancing+software";

            serviceTestsFixture.Mocker.GetMock<HttpClient>()
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(() => 
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    responseMessage.Content = new StringContent("Valid Result");

                    return Task.FromResult(responseMessage);
                });

            //Act
            var result = await httpConnector.GetListAsync(resourceUri, query);

            //Assert
            result.Should().NotBeNull();
        }


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


        #endregion
    }
}
