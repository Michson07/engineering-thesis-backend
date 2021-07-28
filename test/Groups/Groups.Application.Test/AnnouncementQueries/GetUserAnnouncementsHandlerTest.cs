using FluentAssertions;
using Groups.Application.AnnouncementQueries;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.AnnouncementQueries
{
    public class GetUserAnnouncementsHandlerTest : GroupsServicesMock
    {
        private readonly GetUserAnnouncementsHandler handler;

        public GetUserAnnouncementsHandlerTest()
        {
            handler = new GetUserAnnouncementsHandler(announcementAggregateRepository);
        }

        [Fact]
        public async Task ShouldReturnAnnouncementsForUserAsync()
        {
            var email = "user@user.com";
            var creator = new Participient(new(email), GroupRoles.Owner);
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> { creator })
                .Build();

            var announcement1 = new AnnouncementAggregateBuilder()
                .WithTitle("First announcement")
                .WithMessage("aaa")
                .WithCreator(creator)
                .WithGroupAggregate(group)
                .Build();

            var announcement2 = new AnnouncementAggregateBuilder()
                .WithTitle("Second announcement")
                .WithMessage("bbb")
                .WithCreator(creator)
                .WithGroupAggregate(group)
                .Build();

            var announcement3 = new AnnouncementAggregateBuilder().Build();

            await announcementAggregateRepository.Add(announcement1);
            await announcementAggregateRepository.Add(announcement2);
            await announcementAggregateRepository.Add(announcement3);

            var response = await handler.Handle(new GetUserAnnouncementsDto { Email = email }, CancellationToken.None);

            var expected = new List<GetUserAnnouncementsView>
            {
                new GetUserAnnouncementsView
                {
                    Id = announcement1.Id.ToString(),
                    Date = announcement1.Date,
                    IsCreator = true,
                    Message = announcement1.Message,
                    Title = announcement1.Title
                },
                new GetUserAnnouncementsView
                {
                    Id = announcement2.Id.ToString(),
                    Date = announcement2.Date,
                    IsCreator = true,
                    Message = announcement2.Message,
                    Title = announcement2.Title
                }
            };

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }
    }
}
