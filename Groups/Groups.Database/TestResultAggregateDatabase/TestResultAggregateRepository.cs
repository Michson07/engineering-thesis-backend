using Core.Database;
using Groups.Database.Migrations;

namespace Groups.Database.TestResultAggregateDatabase
{
    public class TestResultAggregateRepository : AggregateRepository<GroupsDbContext>, ITestResultAggregateRepository
    {
        public TestResultAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(TestResultAggregate question)
        {
            dbContext.Add(question);
        }

        public void Update(TestResultAggregate question)
        {
            dbContext.Update(question);
        }
    }
}
