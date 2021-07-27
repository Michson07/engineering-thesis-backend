using Groups.Domain.ValueObjects;
using Xunit;

namespace Groups.Domain.Test.ValueObjects
{
    public class GroupAccessCodeTest
    {
        [Fact]
        public void ShouldCreateAccessCodeWhichIsRandomToken()
        {
            var accessCode = new GroupAccessCode();

            Assert.Equal(10, accessCode.ToString().Length);
        }
    }
}
