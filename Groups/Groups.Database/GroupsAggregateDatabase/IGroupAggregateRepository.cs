using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.GroupsAggregateDatabase
{
    public interface IGroupAggregateRepository : IAggregateRepository
    {
        public GroupAggregate? Get(string name);
        public GroupAggregate? GetById(string id);
        public Task<GroupAggregate?> GetByCode(string code);
        public Task<GroupAggregate?> GetByName(string name);

        public IEnumerable<GroupAggregate> GetUserGroups(string email);
        public void Add(GroupAggregate group);
        public void Update(GroupAggregate group);
    }
}
