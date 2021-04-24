using Core.Database;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public class UserAggregateDbContext : AggregateDbContext
    {
        public UserAggregateDbContext(DbContextOptions<UserAggregateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAggregate> UserAggregate { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAggregateConfiguration());
        }
    }
}
