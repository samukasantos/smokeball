

using Moq.AutoMock;
using Xunit;

namespace Smokeball.Tests.Services.Base
{
    [CollectionDefinition(nameof(ServicesCollection))]
    public class ServicesCollection : ICollectionFixture<ServicesTestsFixture>
    { }

    public class ServicesTestsFixture
    {
        #region Properties

        public AutoMocker Mocker { get; private set; }

        #endregion

        #region Constructor

        public ServicesTestsFixture()
        {
            Mocker = new AutoMocker();
        }

        #endregion
    }
}
