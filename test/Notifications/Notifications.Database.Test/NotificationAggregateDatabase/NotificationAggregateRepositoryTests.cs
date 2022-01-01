using Core.Database.Test;
using Core.Domain.ValueObjects;
using FluentAssertions;
using Notifications.Database.NotificationAggregateDatabase;
using Notifications.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Notifications.Database.Test.NotificationAggregateDatabase
{
    public class NotificationAggregateRepositoryTests : DatabaseTestConfiguration<NotificationsDbContext>
    {
        private readonly INotificationAggregateRepository notificationRepository;

        public NotificationAggregateRepositoryTests()
        {
            notificationRepository = new NotificationAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddNotificationToDatabase()
        {
            var eventId = Guid.NewGuid();
            var email = new Email("user@mail.com");
            var notification = NotificationAggregate.Create(eventId, email);

            await notificationRepository.Add(notification);
            await notificationRepository.SaveChanges();

            var notificationInDb = dbContext.NotificationAggregate.Single();
            notificationInDb.Should().BeEquivalentTo(notification);
        }

        [Fact]
        public async Task ShouldGetNotificationFromDatabase()
        {
            var eventId = Guid.NewGuid();
            var email = new Email("user@mail.com");
            var notification = NotificationAggregate.Create(eventId, email);

            dbContext.Add(notification);
            await notificationRepository.SaveChanges();

            var notificationInDb = await notificationRepository.GetByEventId(eventId.ToString());
            notificationInDb.Should().BeEquivalentTo(notification);
        }
    }
}
