using Core.Application.Test;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Test.fakes;
using Notifications.Database.NotificationAggregateDatabase;

namespace Notifications.Application.Test
{
    public class NotificationServicesMock : ServicesMock
    {
        protected INotificationAggregateRepository repository = new NotificationAggregateRepositoryFake();

        protected NotificationServicesMock()
        {
            var services = new ServiceCollection();

            services.AddSingleton(repository);
        }
    }
}
