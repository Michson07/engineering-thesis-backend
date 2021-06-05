using Notifications.Application.NotificationCommands;
using Notifications.Database;
using Notifications.Database.NotificationAggregateDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notifications.Api
{
    public static class DependencyInjection
    {
        public static void AddNotificationsApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR(typeof(SendEmailNotificationHandler).Assembly);
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName("Notifications.Api")));
            services.AddDbContext<NotificationsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Notifications.Database")));
            services.AddScoped<INotificationAggregateRepository, NotificationAggregateRepository>();
        }
    }
}
