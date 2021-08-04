using Core.Database.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.ResourceAggregateDatabase
{
    public class ResourceAggregateConfiguration : IEntityTypeConfiguration<ResourceAggregate>
    {
        public void Configure(EntityTypeBuilder<ResourceAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Url).IsUrlString();
            builder.OwnsOne(o => o.File, file =>
            {
                file.ToTable("File");
                file.WithOwner().HasForeignKey("ResourceId");
                file.Property(f => f.Value);
                file.HasKey("ResourceId", "Name", "Type");
            });
        }
    }
}
