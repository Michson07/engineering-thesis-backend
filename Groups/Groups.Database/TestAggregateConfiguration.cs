using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Groups.Database
{
    public class TestAggregateConfiguration : IEntityTypeConfiguration<TestAggregate>
    {
        public void Configure(EntityTypeBuilder<TestAggregate> builder)
        {
            builder.Ignore(o => o.MaxPoints);
        }
    }
}