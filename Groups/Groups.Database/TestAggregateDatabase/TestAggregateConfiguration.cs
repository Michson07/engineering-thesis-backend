using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.TestAggregateDatabase
{
    public class TestAggregateConfiguration : IEntityTypeConfiguration<TestAggregate>
    {
        public void Configure(EntityTypeBuilder<TestAggregate> builder)
        {
            builder.Ignore(o => o.MaxPoints);
        }
    }
}