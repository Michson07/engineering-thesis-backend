using Core.Database;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
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
            return await dbContext.TestAggregate.Include(test => test.Questions)
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
    }
}
