using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.ResourceAggregateDatabase
{
    public interface IResourceAggregateRepository : IAggregateRepository
    {
        public Task Add(ResourceAggregate resourceAggregate);
        public Task<IEnumerable<ResourceAggregateWithoutFile>> GetGroupResources(string groupId);
        public Task<ResourceAggregate> GetResourceById(string id);
    }
}
