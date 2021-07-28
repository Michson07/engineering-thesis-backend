using Groups.Database.QuestionAggregateDatabase;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Application.Test.fakes
{
    public class QuestionAggregateRepositoryFake : IQuestionAggregateRepository
    {
        private readonly List<QuestionAggregate> repository = new List<QuestionAggregate>();

        public Task Add(IEnumerable<QuestionAggregate> questions)
        {
            foreach (var question in questions)
            {
                Add(question);
            }

            return Task.CompletedTask;
        }

        public Task Add(QuestionAggregate question)
        {
            repository.Add(question);

            return Task.CompletedTask;
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(QuestionAggregate question)
        {
            //
        }
    }
}
