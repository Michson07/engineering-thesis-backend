using Core.Database.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Groups.Database.TestResultAggregateDatabase
{
    public class TestResultAggregateConfiguration : IEntityTypeConfiguration<TestResultAggregate>
    {
        public void Configure(EntityTypeBuilder<TestResultAggregate> builder)
        {
            builder.OwnsOne(o => o.Student, student =>
            {
                student.Property(student => student.Email).IsEmail();
                student.Ignore(student => student.Role);
            });

            builder.OwnsMany(o => o.StudentAnswers, sa =>
            {
                sa.ToTable("StudentAnswer");
                sa.Property<Guid>("Id").IsRequired();
                sa.HasKey("Id");
                sa.Property(sa => sa.ReceivedAnswers)
                    .HasConversion(new StringListToStringValueConverter());
            });
            builder.Ignore(o => o.ReceivedPoints);
        }
    }
}