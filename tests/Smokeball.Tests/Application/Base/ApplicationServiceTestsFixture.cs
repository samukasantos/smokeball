using Bogus;
using Moq.AutoMock;
using Smokeball.WPF.Application.Service.DTO;
using Smokeball.WPF.Presentation.ViewModel;
using Smokeball.WPF.Presentation.ViewModel.Adapter;
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
