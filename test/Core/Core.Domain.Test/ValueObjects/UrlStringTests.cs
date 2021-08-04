using Core.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Core.Domain.Test.ValueObjects
{
    public class UrlStringTests
    {
        [Theory]
        [InlineData("http://abc.com")]
        [InlineData("https://abc.com")]
        [InlineData("https://abc.com/abc")]
        [InlineData("http://abc.pl/1234?abc=abcd")]
        public void ShouldCreateUrlString(string url)
        {
            var urlString = new UrlString(url);

            Assert.Equal(url, urlString);
        }

        [Theory]
        [InlineData("/abc.com")]
        [InlineData("httpsw://abc")]
        [InlineData("abc")]
        [InlineData("http:abc.com/1234?abc=abcd")]
        public void ShouldNotCreateUrlStringWhenFormatIsInvalid(string url)
        {
            Assert.Throws<ValidationException>(() => new UrlString(url));
        }
    }
}
