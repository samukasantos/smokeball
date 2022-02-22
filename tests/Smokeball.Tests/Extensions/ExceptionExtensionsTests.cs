
using FluentAssertions;
using Smokeball.Core.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Smokeball.Tests.Extensions
{
    public class ExceptionExtensionsTests
    {
        [Fact(DisplayName = "Exception Extensions")]
        [Trait("Category", "Extension Methods")]
        public void Given_Exception_When_HasInnerExceptions_Then_ShoulReturnEmptyValueConcatenedtStringValue()
        {
            //Arrange
            var aggregateException = new AggregateException(new List<Exception> 
            {
                new ArgumentNullException(),
                new NotImplementedException()
            });

            //Act
            var result = aggregateException.GetAllMessages();

            //Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
