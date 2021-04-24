using Microsoft.EntityFrameworkCore;

namespace Core.Database
{
    public abstract class AggregateDbContext : DbContext
    {
        protected AggregateDbContext() : base()
        {
        }

        protected AggregateDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
