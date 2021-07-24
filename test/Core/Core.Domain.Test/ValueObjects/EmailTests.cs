using Core.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Core.Domain.Test.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("email@mail.com")]
        [InlineData("user@gmail.pl")]
        [InlineData("address@o2.pl")]
        public void ShouldCreateEmail(string email)
        {
            var emailObj = new Email(email);

            Assert.Equal(email, emailObj);
        }

        [Theory]
        [InlineData("emaill.com")]
        [InlineData("user@")]
        [InlineData("address")]
        public void ShouldThrowExceptionWhenItIsNotAnEmail(string email)
        {
            Assert.Throws<ValidationException>(() => new Email(email));
        }
    }
}
