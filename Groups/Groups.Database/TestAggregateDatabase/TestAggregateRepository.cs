using Core.Database;
using Groups.Domain.Aggregates;
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
