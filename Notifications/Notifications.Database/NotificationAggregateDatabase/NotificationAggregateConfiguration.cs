using Core.Database.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain;

namespace Notifications.Database.NotificationAggregateDatabase
{
    public class NotificationAggregateConfiguration : IEntityTypeConfiguration<NotificationAggregate>
    {
        public void Configure(EntityTypeBuilder<NotificationAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.UserEmail).IsEmail();
        }
    }
}
