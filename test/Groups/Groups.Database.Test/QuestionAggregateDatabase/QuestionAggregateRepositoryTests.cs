using Core.Database.Test;
using FluentAssertions;
using Groups.Database.QuestionAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.QuestionAggregateDatabase
{
    public class QuestionAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly IQuestionAggregateRepository questionAggregateRepository;

        public QuestionAggregateRepositoryTests()
        {
            questionAggregateRepository = new QuestionAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddQuestionToDatabase()
        {
            var question = new QuestionAggregateBuilder().Build();

            await questionAggregateRepository.Add(question);
            await questionAggregateRepository.SaveChanges();

            var questionInDb = dbContext.QuestionAggregate.Single();
            questionInDb.Should().BeEquivalentTo(question);
        }

        [Fact]
        public async Task ShouldAddManyQuestionsToDatabase()
        {
            var question1 = new QuestionAggregateBuilder()
                .WithTitle("Question 1")
                .Build();

            var question2 = new QuestionAggregateBuilder()
                .WithTitle("Question 2")
                .Build();

            var questions = new QuestionAggregate[] { question1, question2 };

            await questionAggregateRepository.Add(questions);
            await questionAggregateRepository.SaveChanges();

            var questionsInDb = dbContext.QuestionAggregate.ToArray();
            questionsInDb.Should().BeEquivalentTo(questions);
        }
    }
}
