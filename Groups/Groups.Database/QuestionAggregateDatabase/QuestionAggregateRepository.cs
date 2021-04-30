using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.QuestionAggregateDatabase
{
    public class QuestionAggregateRepository : AggregateRepository<GroupsDbContext>, IQuestionAggregateRepository
    {
        public QuestionAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(QuestionAggregate question)
        {
            await dbContext.AddAsync(question);
        }

        public async Task Add(IEnumerable<QuestionAggregate> questions)
        {
            foreach(var question in questions)
            {
                await dbContext.AddAsync(question);
            }
        }

        public void Update(QuestionAggregate question)
        {
            dbContext.Update(question);
        }
    }
}
