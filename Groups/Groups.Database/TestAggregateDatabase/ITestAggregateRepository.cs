using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.TestAggregateDatabase
{
    public interface ITestAggregateRepository : IAggregateRepository
    {
        public Task Add(TestAggregate test);
        public void Update(TestAggregate test);
        public IEnumerable<TestAggregate>? GetGroupTests(string name);
        public Task<TestAggregate?> GetTestById(string id);
    }
}
