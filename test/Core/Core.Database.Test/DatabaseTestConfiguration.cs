using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Database.Test
{
    public class DatabaseTestConfiguration<TDbContext> : IDisposable where TDbContext : AggregateDbContext
    {
        protected readonly TDbContext dbContext;

        public DatabaseTestConfiguration()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TDbContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=EngineeringThesis_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                   .UseInternalServiceProvider(serviceProvider);

            dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), builder.Options);
            dbContext.Database.Migrate();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}
