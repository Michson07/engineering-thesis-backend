using Core.Database;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Database.GroupsAggregateDatabase
{
    public class GroupAggregateRepository : AggregateRepository<GroupsDbContext>, IGroupAggregateRepository
    {
        public GroupAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(GroupAggregate group)
        {
            dbContext.Add(group);
        }

        public async Task<GroupAggregate?> GetByCode(string code)
        {
            return await dbContext.GroupAggregate
                .Include(g => g.Participients)
                .SingleOrDefaultAsync(g => g.Code != null && g.Code == code);
        }

        public GroupAggregate? GetById(string id)
        {
            return dbContext
                .GroupAggregate
                .Include(group => group.Participients)
                .FirstOrDefault(group => group.Id.ToString() == id);
        }

        public async Task<GroupAggregate?> GetByName(string name)
        {
            return await dbContext.GroupAggregate.Include(g => g.Participients).FirstOrDefaultAsync(g => g.GroupName == name);
        }

        public async Task<IEnumerable<GroupAggregate>> GetUserGroups(string email)
        {
            return await dbContext
                .GroupAggregate
                .Where(group => group.Participients.Any(p => p.Email == email))
                .ToListAsync();
        }

        public void Update(GroupAggregate group)
        {
            dbContext.Update(group);
        }
    }
}
