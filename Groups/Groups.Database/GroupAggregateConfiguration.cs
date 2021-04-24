using Core.Database.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Groups.Database
{
    public class GroupAggregateConfiguration : IEntityTypeConfiguration<GroupAggregate>
    {
        public void Configure(EntityTypeBuilder<GroupAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsOne(o => o.GroupName).Property(name => name.Name).HasColumnName("GroupName").IsRequired();
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
