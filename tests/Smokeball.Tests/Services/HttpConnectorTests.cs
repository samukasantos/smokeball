

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

        [Fact(DisplayName = "HttpConnector")]
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

        #endregion
    }
}
