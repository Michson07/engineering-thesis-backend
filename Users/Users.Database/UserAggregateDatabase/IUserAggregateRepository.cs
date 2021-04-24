using Core.Database;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public interface IUserAggregateRepository : IAggregateRepository
    {
        public UserAggregate? Get(string email);
        public void Add(UserAggregate user);
        public void Update(UserAggregate user);
    }
}
