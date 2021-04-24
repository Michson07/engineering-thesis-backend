using Core.Database;
using Groups.Domain.Aggregates;

namespace Groups.Database.GroupsAggregateDatabase
{
    public interface IGroupAggregateRepository : IAggregateRepository
    {
        public GroupAggregate? Get(string name);
        public void Add(GroupAggregate group);
        public void Update(GroupAggregate group);
    }
}
