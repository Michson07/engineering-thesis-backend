using Microsoft.EntityFrameworkCore;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public class UserAggregateDbContext : DbContext
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
