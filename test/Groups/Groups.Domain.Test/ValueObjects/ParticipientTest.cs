using Core.Domain.ValueObjects;
using FluentValidation;
using Groups.Domain.ValueObjects;
using Xunit;

namespace Groups.Domain.Test.ValueObjects
{
    public class ParticipientTest
    {
        [Theory]
        [InlineData("aaa")]
        [InlineData("owner")]
        [InlineData("student")]
        public void ShouldNotAllowToCreateParticipientWhoIsNotHavingRoleOwnerOrStudent(string role)
        {
            Assert.Throws<ValidationException>(() => new Participient(new Email("a@a.com"), role));
        }
    }
}
