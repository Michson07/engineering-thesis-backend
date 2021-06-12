using Chat.Database.GroupChatAggregateDatabase;
using Chat.Database.PrivateChatAggregateDatabase;
using Chat.Domain.Aggregates;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Chat.Database
{
    public class ChatDbContext : AggregateDbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupChatAggregate> GroupChatAggregate { get; set; }
        public virtual DbSet<PrivateChatAggregate> PrivateChatAggregate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupChatAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new PrivateChatAggregateConfiguration());
        }
    }
}
