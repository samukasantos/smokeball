using Moq.AutoMock;
using System;
using Xunit;

namespace Smokeball.Tests.Queries.Base
{
    [CollectionDefinition(nameof(QueriesCollection))]
    public class QueriesCollection : ICollectionFixture<QueriesTestsFixture>
    { }

    public class QueriesTestsFixture : IDisposable
    {
        #region Properties

        public AutoMocker Mocker { get; private set; }

        #endregion

        #region Constructor

        public QueriesTestsFixture()
        {
            Mocker = new AutoMocker();
        }

        #endregion

        #region Methods

        public string CreateHmtlContent() 
        {
            return "<h3 class=\"CxTR3 dT86r9\"><div class=\"tRi92s JqpRlm8\">Dummy Test</div></h3><h3 class=\"CaBsR3E3 dTxSXr9\"><div class=\"t1294xs Jqiefk8\">Dummy Test 2</div></h3><h3 class=\"CsDR892-3 dtkchw\"><div class=\"t9olks poq12\">Dummy Test 3</div></h3>";
        }

        public void Dispose() { } 

        #endregion
    }
}
