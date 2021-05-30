using Core.Database.ValueObjects;
using Groups.Database.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Groups.Database.GroupAggregateDatabase
{
    public class GroupAggregateConfiguration : IEntityTypeConfiguration<GroupAggregate>
    {
        public void Configure(EntityTypeBuilder<GroupAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.GroupName).IsGroupName().IsRequired();
            builder.Property(o => o.Code).IsGroupCode();
            builder.OwnsMany(o => o.Participients, participients =>
            {
                participients.ToTable("Participient");
                participients.Property<Guid>("Id").IsRequired();
                participients.HasKey("Id");
                participients.WithOwner().HasForeignKey("GroupId");
                participients.Property(participient => participient.Email).IsEmail();
                participients.Property(p => p.Role).HasColumnName("Role").IsRequired();
            });
        }
    }
}
