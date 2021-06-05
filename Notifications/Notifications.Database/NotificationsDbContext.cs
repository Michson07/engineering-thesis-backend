using Core.Database;
using Microsoft.EntityFrameworkCore;
using Notifications.Database.NotificationAggregateDatabase;
using Notifications.Domain;

namespace Notifications.Database
{
    public class NotificationsDbContext : AggregateDbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NotificationAggregate> NotificationAggregate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotificationAggregateConfiguration());
        }
    }
}
