using Groups.Database.TestAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Application.Test.fakes
{
    public class TestAggregateRepositoryFake : ITestAggregateRepository
    {
        private readonly List<TestAggregate> repository = new List<TestAggregate>();

        public Task Add(TestAggregate test)
        {
            repository.Add(test);

            return Task.CompletedTask;
        }

        public IEnumerable<TestAggregate> GetAllUserTests(string email)
        {
            return repository.Where(test => test.Group.Participients.Any(user => user.Email == email));
        }

        public Task<IEnumerable<TestAggregate>> GetFutureTests(DateTime fromDate)
        {
            return Task.FromResult(repository.Where(t => t.Date.CompareTo(fromDate) > 0));
        }

        public IEnumerable<TestAggregate> GetGroupTests(string name)
        {
            return repository.Where(test => test.Group.GroupName == name);
        }

        public Task<TestAggregate> GetTestById(string id)
        {
            return Task.FromResult(repository.FirstOrDefault(test => test.Id.ToString() == id));
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(TestAggregate test)
        {
            // not needed
        }

        public Task<bool> UserIsAllowedToCheckResults(string testId, string email)
        {
            return Task.FromResult(
                repository.Any(t => t.Id.ToString() == testId
                && t.Group.Participients.Any(p => p.Email == email && p.Role == GroupRoles.Owner))
            );
        }
    }
}
