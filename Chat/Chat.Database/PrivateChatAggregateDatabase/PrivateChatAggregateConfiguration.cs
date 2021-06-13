using Chat.Domain.Aggregates;
using Core.Database.ValueObjects;
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
            builder.Property(o => o.User1Email).IsEmail();
            builder.Property(o => o.User2Email).IsEmail();
            builder.OwnsMany(o => o.Messages, messages =>
            {
                messages.ToTable("PrivateChatMessages");
                messages.Property<Guid>("Id").IsRequired();
                messages.HasKey("Id");
                messages.Property(message => message.UserEmail).IsEmail();
                messages.WithOwner().HasForeignKey("PrivateChatId");
            });
        }
    }
}
