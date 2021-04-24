using Core.Database;
using System.Linq;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public class UserAggregateRepository : AggregateRepository<UserAggregateDbContext>, IUserAggregateRepository
    {
        public UserAggregateRepository(UserAggregateDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(UserAggregate user)
        {
            dbContext.Add(user);
        }

        public UserAggregate? Get(string email)
        {
            return dbContext.UserAggregate.FirstOrDefault(u => u.Email == email);
        }

        public void Update(UserAggregate user)
        {
            dbContext.Update(user);
        }
    }
}
