using Core.Database;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Database.TestAggregateDatabase
{
    public class TestAggregateRepository : AggregateRepository<GroupsDbContext>, ITestAggregateRepository
    {
        public TestAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(TestAggregate test)
        {
            await dbContext.AddAsync(test);
        }

        public async Task<TestAggregate>? GetTestById(string id)
        {
            return await dbContext
                .TestAggregate
                .Include(test => test.Questions)
                .Include(test => test.Group)
                .FirstOrDefaultAsync(test => test.Id.ToString() == id);
        }

        public IEnumerable<TestAggregate>? GetGroupTests(string name)
        {
            return dbContext.TestAggregate.Where(test => test.Group.GroupName == name);
        }

        public void Update(TestAggregate test)
        {
            dbContext.Update(test);
        }

        public async Task<bool> UserIsAllowedToCheckResults(string testId, string email)
        {
            return await dbContext
                .TestAggregate
                .Include(t => t.Group)
                .Include(t => t.Group.Participients)
                .AnyAsync(t => t.Id.ToString() == testId && t.Group.Participients.Any(p => p.Email == email && p.Role == GroupRoles.Owner));
        }

        public IEnumerable<TestAggregate> GetAllUserTests(string email)
        {
            return dbContext
                .TestAggregate
                .Include(t => t.Group)
                .Include(t => t.Group.Participients)
                .Where(t => t.Group.Participients.Any(p => p.Email == email));
        }

        public async Task<IEnumerable<TestAggregate>> GetFutureTests(DateTime fromDate)
        {
            return await dbContext
                .TestAggregate
                .Include(t => t.Group)
                .Include(t => t.Group.Participients)
                .Where(t => t.Date.CompareTo(fromDate) > 0)
                .ToListAsync();
        }
    }
}
