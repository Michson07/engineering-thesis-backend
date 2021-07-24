using Core.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Core.Domain.Test.ValueObjects
{
    public class PhoneNumbersTests
    {
        [Theory]
        [InlineData("+48111222333")]
        [InlineData("111222333")]
        [InlineData("111 222 333")]
        [InlineData("+48 111 222 333")]
        [InlineData("111 222 3")]
        public void ShouldCreatePhoneNumber(string phoneNumber)
        {
            var phoneNumberObj = new PhoneNumber(phoneNumber);

            Assert.Equal(phoneNumber, phoneNumberObj.Number);
        }

        [Theory]
        [InlineData("Not a phone number")]
        [InlineData("11122X2333")]
        [InlineData("+48XXXYYYZZZ")]
        public void ShouldThrowExceptionWhenItIsNotAPhoneNumber(string phoneNumber)
        {
            Assert.Throws<ValidationException>(() => new PhoneNumber(phoneNumber));
        }
    }
}
