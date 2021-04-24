using Core.Database;
using Groups.Domain.Aggregates;
using System;

namespace Groups.Database.TestAggregateDatabase
{
    public class TestAggregateRepository : AggregateRepository<GroupsDbContext>, ITestAggregateRepository
    {
        public TestAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(TestAggregate test)
        {
            dbContext.Add(test);
        }

        public void Update(TestAggregate test)
        {
            dbContext.Update(test);
        }
    }
}
