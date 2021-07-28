using Core.Domain.ValueObjects;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Application.Test.fakes
{
    public class TestResultAggregateRepositoryFake : ITestResultAggregateRepository
    {
        private readonly List<TestResultAggregate> repository = new List<TestResultAggregate>();

        public void Add(TestResultAggregate question)
        {
            repository.Add(question);
        }

        public Task<TestResultAggregate> GetTestResult(string email, string testId)
        {
            return Task.FromResult(repository.FirstOrDefault(tr => tr.Student.Email == email && tr.Test.Id.ToString() == testId));
        }

        public Task<IEnumerable<Email>> GetTestStudents(string testId)
        {
            return Task.FromResult(repository.Where(tr => tr.Test.Id.ToString() == testId).Select(test => test.Student.Email));
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(TestResultAggregate question)
        {
            // not needed
        }
    }
}
