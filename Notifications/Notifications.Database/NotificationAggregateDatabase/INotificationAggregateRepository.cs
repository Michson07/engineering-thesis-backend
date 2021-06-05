using Core.Database;
using Notifications.Domain;
using System.Threading.Tasks;

namespace Notifications.Database.NotificationAggregateDatabase
{
    public interface INotificationAggregateRepository : IAggregateRepository
    {
        public Task Add(NotificationAggregate notification);
        public Task<NotificationAggregate?> GetByEventId(string eventId);
    }
}
