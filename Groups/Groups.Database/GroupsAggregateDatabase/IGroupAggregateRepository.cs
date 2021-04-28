using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;

namespace Groups.Database.GroupsAggregateDatabase
{
    public interface IGroupAggregateRepository : IAggregateRepository
    {
        public GroupAggregate? Get(string name);
        public IEnumerable<GroupAggregate> GetUserGroups(string email);
        public void Add(GroupAggregate group);
        public void Update(GroupAggregate group);
    }
}
