using System.Threading.Tasks;

namespace Core.Database
{
    public abstract class AggregateRepository<T> where T : AggregateDbContext
    {
        protected readonly T dbContext;

        protected AggregateRepository(T dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
