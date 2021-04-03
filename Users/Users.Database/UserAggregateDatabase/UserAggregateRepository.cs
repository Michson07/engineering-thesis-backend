using System.Linq;
using Users.Domain.Aggregates;

namespace Users.Database.UserAggregateDatabase
{
    public class UserAggregateRepository : IUserAggregateRepository
    {
        private readonly UserAggregateDbContext dbContext;

        public UserAggregateRepository(UserAggregateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(UserAggregate user)
        {
            dbContext.Add(user);
        }

        public UserAggregate? Get(string email)
        {
            return dbContext.UserAggregate.FirstOrDefault(u => u.Email.EmailAddress == email);
        }

        public void Update(UserAggregate user)
        {
            dbContext.Update(user);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
