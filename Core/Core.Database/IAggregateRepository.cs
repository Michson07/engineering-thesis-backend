using System.Threading.Tasks;

namespace Core.Database
{
    public interface IAggregateRepository
    {
        public Task SaveChanges();
    }
}
