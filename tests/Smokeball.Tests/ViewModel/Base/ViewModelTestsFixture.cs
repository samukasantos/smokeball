
using Bogus;
using Moq.AutoMock;
using Smokeball.WPF.Presentation.ViewModel;
using System;
using Xunit;

namespace Smokeball.Tests.ViewModel.Base
{
    [CollectionDefinition(nameof(ViewModelCollection))]
    public class ViewModelCollection : ICollectionFixture<ApplicationServiceTestsFixture>
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

        public SearchViewModel GenerateInvalidSearchViewModel()
        {
            return new SearchViewModel();
        }

        public SearchViewModel GenerateValidSearchViewModel() 
        {
            return new SearchViewModel
            {
                Keyword = new Faker().Random.String2(10)
            };
        }

        public void Dispose() { } 

        #endregion
    }
}
