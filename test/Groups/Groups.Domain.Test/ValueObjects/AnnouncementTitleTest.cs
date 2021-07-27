using Core.Domain.Test;
using FluentValidation;
using Groups.Domain.ValueObjects;
using Xunit;

namespace Groups.Domain.Test.ValueObjects
{
    public class AnnouncementTitleTest
    {
        [Theory]
        [InlineData(101)]
        [InlineData(500)]
        public void ShouldNotAllowToCreateAnnouncementTitleLongerThan100Chars(int length)
        {
            var title = StringGenerator.GenerateStringWithLength(length);

            Assert.Throws<ValidationException>(() => new AnnouncementTitle(title));
        }
    }
}
