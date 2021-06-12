using Chat.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Chat.Database.PrivateChatAggregateDatabase
{
    public class PrivateChatAggregateConfiguration : IEntityTypeConfiguration<PrivateChatAggregate>
    {
        public void Configure(EntityTypeBuilder<PrivateChatAggregate> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsMany(o => o.Messages, messages =>
            {
                messages.Property<Guid>("Id").IsRequired();
                messages.HasKey("Id");
                messages.WithOwner().HasForeignKey("PrivateChatId");
            });
        }
    }
}
