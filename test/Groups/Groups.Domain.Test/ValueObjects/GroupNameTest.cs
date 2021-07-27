using Core.Domain.Test;
using FluentValidation;
using Groups.Domain.ValueObjects;
using Xunit;

namespace Groups.Domain.Test.ValueObjects
{
    public class GroupNameTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldNotAllowToCreateGroupNameShorterThan3Chars(int length)
        {
            var name = StringGenerator.GenerateStringWithLength(length);

            Assert.Throws<ValidationException>(() => new GroupName(name));
        }

        [Theory]
        [InlineData(41)]
        [InlineData(100)]
        public void ShouldNotAllowToCreateGroupNameLongerThan40Chars(int length)
        {
            var name = StringGenerator.GenerateStringWithLength(length);

            Assert.Throws<ValidationException>(() => new GroupName(name));
        }
    }
}
