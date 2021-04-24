using Core.Database;
using Groups.Domain.Aggregates;

namespace Groups.Database.QuestionAggregateDatabase
{
    public interface IQuestionAggregateRepository : IAggregateRepository
    {
        public void Add(QuestionAggregate question);
        public void Update(QuestionAggregate question);
    }
}
