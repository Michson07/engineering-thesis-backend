using Core.Database;
using Groups.Database.Migrations;

namespace Groups.Database.TestResultAggregateDatabase
{
    public interface ITestResultAggregateRepository : IAggregateRepository
    {
        public void Add(TestResultAggregate question);
        public void Update(TestResultAggregate question);
    }
}
