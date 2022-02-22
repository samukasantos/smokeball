using Moq.AutoMock;
using System;
using Xunit;

namespace Smokeball.Tests.Application.Base
{
    [CollectionDefinition(nameof(ApplicationServiceCollection))]
    public class ApplicationServiceCollection : ICollectionFixture<ApplicationServiceTestsFixture>
    { }

    public class ApplicationServiceTestsFixture : IDisposable
    {
        #region Properties

        public AutoMocker Mocker { get; private set; }

        #endregion

        #region Constructor

        public ApplicationServiceTestsFixture()
        {
            Mocker = new AutoMocker();
        }

        #endregion

        #region Methods

        public void Dispose() { } 

        #endregion
    }
}
