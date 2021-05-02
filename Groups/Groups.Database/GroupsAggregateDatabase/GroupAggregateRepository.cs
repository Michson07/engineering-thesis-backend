using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;

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

        public GroupAggregate? Get(string name)
        {
            return dbContext.GroupAggregate.FirstOrDefault(group => group.GroupName == name);
        }

        public GroupAggregate? GetById(string id)
        {
            return dbContext.GroupAggregate.FirstOrDefault(group => group.Id.ToString() == id);
        }

        public IEnumerable<GroupAggregate> GetUserGroups(string email)
        {
            return dbContext
                .GroupAggregate
                .Where(group => group.Participients.Any(p => p.Email == email));
        }

        public void Update(GroupAggregate group)
        {
            dbContext.Update(group);
        }
    }
}
