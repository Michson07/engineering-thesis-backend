using Core.Domain.ValueObjects;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Groups.Domain.Test.Aggregates
{
    public class GroupAggregateTest
    {
        [Fact]
        public void ShouldCreateGroup()
        {
            var group = new GroupAggregateBuilder().Build();

            Assert.True(group.Participients.Count() == 1);
            Assert.Equal("Group1", group.GroupName);
            Assert.Equal("Some description of Group1", group.Description);
            Assert.Null(group.Code);
        }

        [Fact]
        public void ShouldUpdateGroup()
        {
            var group = new GroupAggregateBuilder().Build();
            group.Update(group.Participients, group.GroupName, "new description", false);

            Assert.True(group.Participients.Count() == 1);
            Assert.Equal("Group1", group.GroupName);
            Assert.Equal("new description", group.Description);
            Assert.NotNull(group.Code);
        }

        [Fact]
        public void ShouldNotAllowToCreateGroupWhenIsEmpty()
        {
            Assert.Throws<ValidationException>(() => new GroupAggregateBuilder()
               .WithParticipients(new List<Participient>())
               .Build()
           );
        }

        [Fact]
        public void ShouldNotAllowToCreateGroupDoesNotHaveOwner()
        {
            var student = new Participient(new Email("student@student.pl"), GroupRoles.Student);

            Assert.Throws<ValidationException>(() => new GroupAggregateBuilder()
               .WithParticipients(new List<Participient>() { student })
               .Build()
           );
        }

        [Fact]
        public void ShouldNotAllowToUpdateGroupWhenIsEmpty()
        {
            var group = new GroupAggregateBuilder().Build();

            Assert.Throws<ValidationException>(() => 
                group.Update(
                    new List<Participient>(), 
                    group.GroupName,
                    group.Description,
                    false
                )
           );
        }

        [Fact]
        public void ShouldNotAllowToUpdateGroupDoesNotHaveOwner()
        {
            var group = new GroupAggregateBuilder().Build();
            var student = new Participient(new Email("student@student.pl"), GroupRoles.Student);

            Assert.Throws<ValidationException>(() => 
                group.Update(
                    new List<Participient>() { student },
                    group.GroupName,
                    group.Description,
                    false
                )
           );
        }
    }
}
