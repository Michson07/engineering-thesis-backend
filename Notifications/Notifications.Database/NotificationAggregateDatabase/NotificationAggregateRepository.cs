using Core.Database;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain;
using System.Threading.Tasks;

namespace Notifications.Database.NotificationAggregateDatabase
{
    public class NotificationAggregateRepository : AggregateRepository<NotificationsDbContext>, INotificationAggregateRepository
    {

        public NotificationAggregateRepository(NotificationsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(NotificationAggregate notification)
        {
            await dbContext.AddAsync(notification);
        }

        public async Task<NotificationAggregate?> GetByEventId(string eventId)
        {
            return await dbContext.NotificationAggregate.FirstOrDefaultAsync(n => n.EventId.ToString() == eventId);
        }
    }
}
