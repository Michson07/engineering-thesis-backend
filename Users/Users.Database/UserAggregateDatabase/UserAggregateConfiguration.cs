using Core.Database.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public class UserAggregateConfiguration : IEntityTypeConfiguration<UserAggregate>
    {
        public void Configure(EntityTypeBuilder<UserAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Email).IsEmail().IsRequired();
            builder.OwnsOne(o => o.Photo).Property(photo => photo.Image).HasColumnName("Photo");
        }
    }
}
