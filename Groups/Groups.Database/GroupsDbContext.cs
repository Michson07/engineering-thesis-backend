using Core.Database;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Database.GroupAggregateDatabase;
using Groups.Database.QuestionAggregateDatabase;
using Groups.Database.ResourceAggregateDatabase;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Groups.Database
{
    public class GroupsDbContext : AggregateDbContext
    {
        public GroupsDbContext(DbContextOptions<GroupsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupAggregate> GroupAggregate { get; set; }
        public virtual DbSet<QuestionAggregate> QuestionAggregate { get; set; }
        public virtual DbSet<TestAggregate> TestAggregate { get; set; }
        public virtual DbSet<TestResultAggregate> TestResultAggregate { get; set; }
        public virtual DbSet<AnnouncementAggregate> AnnouncementAggregate { get; set; }
        public virtual DbSet<ResourceAggregate> ResourceAggregate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new TestAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new TestResultAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new AnnouncementAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceAggregateConfiguration());
        }
    }
}
