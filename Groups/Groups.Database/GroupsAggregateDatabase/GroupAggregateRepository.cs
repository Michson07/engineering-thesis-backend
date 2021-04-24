using Core.Database;
using Groups.Domain.Aggregates;
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

        public void Update(GroupAggregate group)
        {
            dbContext.Update(group);
        }
    }
}
