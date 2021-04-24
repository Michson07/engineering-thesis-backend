using Core.Database;
using Groups.Domain.Aggregates;

namespace Groups.Database.QuestionAggregateDatabase
{
    public class QuestionAggregateRepository : AggregateRepository<GroupsDbContext>, IQuestionAggregateRepository
    {
        public QuestionAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(QuestionAggregate question)
        {
            dbContext.Add(question);
        }

        public void Update(QuestionAggregate question)
        {
            dbContext.Update(question);
        }
    }
}
