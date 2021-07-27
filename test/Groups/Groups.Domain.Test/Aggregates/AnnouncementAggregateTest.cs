using Core.Domain.ValueObjects;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Xunit;

namespace Groups.Domain.Test.Aggregates
{
    public class AnnouncementAggregateTest
    {
        [Fact]
        public void ShouldCreateAnnouncement()
        {
            var announcement = new AnnouncementAggregateBuilder().Build();

            Assert.Equal("Title", announcement.Title);
            Assert.Equal("Message", announcement.Message);
            Assert.NotNull(announcement.Group);
            Assert.Equal(new Participient(new Email("creator@mail.com"), GroupRoles.Owner), announcement.Creator);
        }

        [Fact]
        public void ShouldNotCreateAnnouncementIfCreatorIsNotInTheGroup()
        {
            var exception = Assert.Throws<Exception>(() => new AnnouncementAggregateBuilder()
                .WithCreator(new Participient(new Email("notFromThisGroup@mail.com"), GroupRoles.Owner))
                .Build()
            );

            Assert.Equal("Twórca nie należy do podanej grupy!", exception.Message);
        }

        [Fact]
        public void ShouldNotCreateAnnouncementIfCreatorIsNotOwnerOfTheGroup()
        {
            var owner = new Participient(new Email("owner@mail.com"), GroupRoles.Owner);
            var student = new Participient(new Email("notOwner@mail.com"), GroupRoles.Student);
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient>() 
                { 
                    owner,
                    student 
                })
                .Build();

            Assert.Throws<ValidationException>(() => new AnnouncementAggregateBuilder()
                .WithCreator(student)
                .WithGroupAggregate(group)
                .Build()
            );
        }
    }
}
