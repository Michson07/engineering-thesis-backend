using Core.Application.Test;
using Notifications.Application.Test.fakes;
using Notifications.Database.NotificationAggregateDatabase;

namespace Notifications.Application.Test
{
    public class NotificationServicesMock : ServicesMock
    {
        protected INotificationAggregateRepository repository = new NotificationAggregateRepositoryFake();
    }
}
