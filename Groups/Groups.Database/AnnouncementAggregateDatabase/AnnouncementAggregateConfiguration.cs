using Core.Database.ValueObjects;
using Groups.Database.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.AnnouncementAggregateDatabase
{
    public class AnnouncementAggregateConfiguration : IEntityTypeConfiguration<AnnouncementAggregate>
    {
        public void Configure(EntityTypeBuilder<AnnouncementAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Title).IsAnnouncementTitle();
            builder.Property(o => o.Message).IsAnnouncementMessage();
            builder.OwnsOne(o => o.Creator, creator =>
            {
                creator.Property(creator => creator.Email).IsEmail();
                creator.Ignore(creator => creator.Role);
            });
        }
    }
}
