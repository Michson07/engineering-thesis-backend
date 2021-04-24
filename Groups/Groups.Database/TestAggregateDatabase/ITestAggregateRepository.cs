using Core.Database;
using Groups.Domain.Aggregates;

namespace Groups.Database.TestAggregateDatabase
{
    public interface ITestAggregateRepository : IAggregateRepository
    {
        public void Add(TestAggregate test);
        public void Update(TestAggregate test);
    }
}
