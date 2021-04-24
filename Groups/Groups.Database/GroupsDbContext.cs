using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Groups.Database
{
    public class GroupsDbContext : DbContext
    {
        public GroupsDbContext(DbContextOptions<GroupsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupAggregate> GroupAggregate { get; set; }
        public virtual DbSet<QuestionAggregate> QuestionAggregate { get; set; }
        public virtual DbSet<TestAggregate> TestAggregate { get; set; }
        public virtual DbSet<TestResultAggregate> TestResultAggregate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new TestAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new TestResultAggregateConfiguration());
        }
    }
}
