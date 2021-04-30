using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.QuestionAggregateDatabase
{
    public interface IQuestionAggregateRepository : IAggregateRepository
    {
        public Task Add(IEnumerable<QuestionAggregate> questions);
        public Task Add(QuestionAggregate question);
        public void Update(QuestionAggregate question);
    }
}
