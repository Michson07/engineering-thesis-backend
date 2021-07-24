using Notifications.Database.NotificationAggregateDatabase;
using Notifications.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Application.Test.fakes
{
    public class NotificationAggregateRepositoryFake : INotificationAggregateRepository
    {
        private readonly List<NotificationAggregate> notifications = new List<NotificationAggregate>();

        public Task Add(NotificationAggregate notification)
        {
            notifications.Add(notification);

            return Task.CompletedTask;
        }

        public Task<NotificationAggregate> GetByEventId(string eventId)
        {
            var notification = notifications.FirstOrDefault(n => n.EventId.ToString().Equals(eventId, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(notification);
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }
    }
}
