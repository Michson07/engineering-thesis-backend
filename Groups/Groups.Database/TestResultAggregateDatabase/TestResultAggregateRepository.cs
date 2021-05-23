using Core.Database;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public async Task<TestResultAggregate?> GetTestResult(string email, string testId)
        {
            return await dbContext
                .TestResultAggregate
                .Include(tr => tr.Student)
                .Include(tr => tr.StudentAnswers)
                .Include(tr => tr.Test)
                .Include(tr => tr.Test.Questions)
                .FirstOrDefaultAsync(tr => tr.Student.Email == email && tr.Test.Id.ToString() == testId);
        }

        public void Update(TestResultAggregate question)
        {
            dbContext.Update(question);
        }
    }
}
