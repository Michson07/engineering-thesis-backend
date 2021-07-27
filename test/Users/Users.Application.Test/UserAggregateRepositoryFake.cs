using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;
using Users.Domain.Aggregates;

namespace Users.Application.Test
{
    public class UserAggregateRepositoryFake : IUserAggregateRepository
    {
        private readonly List<UserAggregate> users = new List<UserAggregate>();

        public void Add(UserAggregate user)
        {
            users.Add(user);
        }

        public UserAggregate Get(string email)
        {
            return users.FirstOrDefault(user => user.Email == email);
        }

        public Task<IEnumerable<UserAggregate>> GetAllUsersAsync()
        {
            return Task.FromResult((IEnumerable<UserAggregate>)users);
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(UserAggregate user)
        {
        }
    }
}
