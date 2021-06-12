using Chat.Domain.Aggregates;
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
                messages.Property<Guid>("Id").IsRequired();
                messages.HasKey("Id");
                messages.WithOwner().HasForeignKey("ChatGroupId");
            });
        }
    }
}
