
using FluentAssertions;
using Smokeball.WPF.Extensions;
using Xunit;

namespace Smokeball.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact(DisplayName = "String Extensions Not Empty Value")]
        [Trait("Category", "Extension Methods")]
        public void Given_StringValueNotEmptyOrNull_When_ConvertToQueryParameterIsCalled_Then_ShouldFormatStringAsQueryString()
        {
            //Arrange
            var query = "conveyancing,software";
            var queryExpected = "conveyancing+software";

            //Act
            var result = query.ConvertToQueryParameter();

            //Assert
            result.Should().BeEquivalentTo(queryExpected);
        }

        [Fact(DisplayName = "String Extensions Empty Value")]
        [Trait("Category", "Extension Methods")]
        public void Given_StringValueIsEmptyOrNull_When_ConvertToQueryParameterIsCalled_Then_ShoulReturnEmptyValue()
        {
            //Arrange
            var query = "";

            //Act
            var result = query.ConvertToQueryParameter();

            //Assert
            result.Should().BeNullOrEmpty();
        }
    }
}
