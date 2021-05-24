using Core.Database;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.TestResultAggregateDatabase
{
    public interface ITestResultAggregateRepository : IAggregateRepository
    {
        public void Add(TestResultAggregate question);
        public void Update(TestResultAggregate question);
        public Task<TestResultAggregate?> GetTestResult(string email, string testId);
        public Task<IEnumerable<Email>> GetTestStudents(string testId);
    }
}
