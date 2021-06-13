using Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<UserAggregate>> GetAllUsersAsync()
        {
            return await dbContext.UserAggregate.ToListAsync();
        }

        public void Update(UserAggregate user)
        {
            dbContext.Update(user);
        }
    }
}
