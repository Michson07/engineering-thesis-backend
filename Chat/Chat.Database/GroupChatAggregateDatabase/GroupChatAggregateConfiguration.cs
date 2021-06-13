using Chat.Domain.Aggregates;
using Core.Database.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Chat.Database.GroupChatAggregateDatabase
{
    public class GroupChatAggregateConfiguration : IEntityTypeConfiguration<GroupChatAggregate>
    {
        public void Configure(EntityTypeBuilder<GroupChatAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsMany(o => o.Messages, messages =>
            {
                messages.ToTable("GroupChatMessages");
                messages.Property<Guid>("Id").IsRequired();
                messages.HasKey("Id");
                messages.Property(message => message.UserEmail).IsEmail();
                messages.WithOwner().HasForeignKey("ChatGroupId");
            });
        }
    }
}
