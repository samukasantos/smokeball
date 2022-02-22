

using FluentAssertions;
using Smokeball.Application.Service.Configuration;
using Smokeball.Application.Service.DTO;
using Smokeball.Core.Extensions;
using Xunit;

namespace Smokeball.Tests.Extensions
{
    public class HtmlExtensionsTests
    {
        [Fact(DisplayName = "Html Extensions Matched Results")]
        [Trait("Category", "Extension Methods")]
        public void Given_HtmlContent_When_RegexIsApplied_Then_ShouldMatchAndReturnValidValues()
        {
            //Arrange
            var regexPatterns = new GoogleRegexPatterns { GoogleH3Pattern = "<h3 class=\".*?\"><div class=\".*?\">([^<]+)</div></h3>"  };
            var html = "<h3 class=\"CsDR3 dTxr9\"><div class=\"tRi92s JqpRlm8\">Dummy Test</div></h3>";

            //Act
            var titles = html.ExtractTitleContent<ResultDto>(regexPatterns.GoogleH3Pattern);

            //Assert
            titles.Should().NotBeNull();
            titles.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Html Extensions Not Matched Results")]
        [Trait("Category", "Extension Methods")]
        public void Given_HtmlContent_When_RegexIsApplied_Then_ShouldReturnEmptyWhenNotMatchValidValues()
        {
            //Arrange
            var regexPatterns = new GoogleRegexPatterns { GoogleH3Pattern = "<h3 class=\".*?\"><div class=\".*?\">([^<]+)</div></h3>" };
            var html = "<h2 class=\"CsDR3 dTxr9\"><div class=\"tRi92s JqpRlm8\">Dummy Test</div></h2>";

            //Act
            var titles = html.ExtractTitleContent<ResultDto>(regexPatterns.GoogleH3Pattern);

            //Assert
            titles.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Html Extensions Invalid(Empty) Regex")]
        [Trait("Category", "Extension Methods")]
        public void Given_HtmlContent_When_RegexEmptyIsApplied_Then_ShouldReturnNullWhenThrowException()
        {
            //Arrange
            var regexPatterns = new GoogleRegexPatterns { GoogleH3Pattern = "" };
            var html = "<h2 class=\"CsDR3 dTxr9\"><div class=\"tRi92s JqpRlm8\">Dummy Test</div></h2>";

            //Act
            var titles = html.ExtractTitleContent<ResultDto>(regexPatterns.GoogleH3Pattern);

            //Assert
            titles.Should().BeNull();
        }
    }
}
