using Core.Api;
using Groups.Application.AnnouncementCommands;
using Groups.Domain.Test.Aggregates;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.AnnouncementCommands
{
    public class UpdateAnnouncementHandlerTest : GroupsServicesMock
    {
        private readonly UpdateAnnouncementHandler handler;

        public UpdateAnnouncementHandlerTest()
        {
            handler = new UpdateAnnouncementHandler(announcementAggregateRepository);
        }

        [Fact]
        public async Task ShouldUpdateAnnouncementAsync()
        {
            var announcement = new AnnouncementAggregateBuilder().Build();

            await announcementAggregateRepository.Add(announcement);

            var request = new UpdateAnnouncementDto
            {
                Id = announcement.Id.ToString(),
                Message = "Updated message",
                Title = "Updated title"
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<OkResult>(response.Result);
            Assert.Equal("Updated message", announcement.Message);
            Assert.Equal("Updated title", announcement.Title);
        }

        [Fact]
        public async Task ShouldFailWhenAnnouncementNotExists()
        {
            var request = new UpdateAnnouncementDto
            {
                Id = "notExistingAnnouncement",
            };

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
