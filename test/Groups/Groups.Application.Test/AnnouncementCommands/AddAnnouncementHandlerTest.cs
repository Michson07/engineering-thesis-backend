using Core.Api;
using Core.Domain.ValueObjects;
using Groups.Application.AnnouncementCommands;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.AnnouncementCommands
{
    public class AddAnnouncementHandlerTest : GroupsServicesMock
    {
        private readonly AddAnnouncementHandler handler;

        public AddAnnouncementHandlerTest()
        {
            handler = new AddAnnouncementHandler(announcementAggregateRepository, groupAggregateRepository);
        }

        [Fact]
        public async Task ShouldAddAnnouncementAsync()
        {
            var email = new Email("mailCreator@mail.com");
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> { new(email, GroupRoles.Owner) })
                .Build();

            groupAggregateRepository.Add(group);

            var request = new AddAnnouncementDto
            {
                CreatorEmail = email,
                GroupId = group.Id.ToString(),
                Message = "Very important announcement for group",
                Title = "title"
            };

            var response = await handler.Handle(request, CancellationToken.None);
            var announcements = await announcementAggregateRepository.GetAnnouncementsForUser(email);

            Assert.IsType<OkResult>(response.Result);
            Assert.Single(announcements);
        }

        [Fact]
        public async Task ShouldNotAllowToCreateAnnouncementForNotExisitingGroup()
        {
            var email = new Email("mailCreator@mail.com");
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> { new(email, GroupRoles.Owner) })
                .Build();

            var request = new AddAnnouncementDto
            {
                CreatorEmail = email,
                GroupId = group.Id.ToString(),
                Message = "Very important announcement for group",
                Title = "title"
            };

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldNotAllowToCreateAnnouncementWhenCreatorNotBelongsToGroup()
        {
            var email = new Email("mailCreator@mail.com");
            var group = new GroupAggregateBuilder().Build();

            var request = new AddAnnouncementDto
            {
                CreatorEmail = email,
                GroupId = group.Id.ToString(),
                Message = "Very important announcement for group",
                Title = "title"
            };

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
