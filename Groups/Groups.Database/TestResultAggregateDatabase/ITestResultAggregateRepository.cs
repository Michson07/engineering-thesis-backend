using Core.Database;
using Groups.Domain.Aggregates;
using System.Threading.Tasks;

namespace Groups.Database.TestResultAggregateDatabase
{
    public interface ITestResultAggregateRepository : IAggregateRepository
    {
        public void Add(TestResultAggregate question);
        public void Update(TestResultAggregate question);
        public Task<TestResultAggregate?> GetTestResult(string email, string testId);
    }
}
