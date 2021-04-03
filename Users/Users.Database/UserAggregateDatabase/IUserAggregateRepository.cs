using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public interface IUserAggregateRepository
    {
        public UserAggregate? Get(string email);
        public void Add(UserAggregate user);
        public void Update(UserAggregate user);
    }
}
