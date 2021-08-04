using FluentAssertions;
using Groups.Application.TestQueries;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestQueries
{
    public class GetTestHandlerTest : GroupsServicesMock
    {
        private readonly GetTestHandler handler;

        public GetTestHandlerTest()
        {
            handler = new GetTestHandler(testAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetTestAsync()
        {
            var test = new TestAggregateBuilder().Build();

            await testAggregateRepository.Add(test);

            var response = await handler.Handle(new() { TestId = test.Id.ToString() }, CancellationToken.None);
            var expected = ExpectedView(test);

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnNullWhenTestDoesNotExist()
        {
            var response = await handler.Handle(new() { TestId = "notExists" }, CancellationToken.None);

            Assert.Null(response.BodyResponse);
        }

        private static TestView ExpectedView(TestAggregate test)
        {
            var question1 = test.Questions.First();
            var question2 = test.Questions.ElementAt(1);
            var question3 = test.Questions.Last();

            return new TestView
            {
                Date = test.Date,
                Name = test.Name,
                PassedFrom = test.PassedFrom,
                RequirePhoto = test.RequirePhoto,
                TimeDuration = test.TimeDuration,
                Questions = new List<QuestionView>
                {
                    new QuestionView
                    {
                        Title = question1.Title,
                        ClosedQuestion = question1.ClosedQuestion,
                        ManyCorrectAnswers = question1.ManyCorrectAnswers,
                        Photo = question1.Photo?.Image,
                        PointsForQuestion = question1.PointsForQuestion,
                        PossibleAnswers = question1.Answers?.Select(a => a.Value)
                    },
                    new QuestionView
                    {
                        Title = question2.Title,
                        ClosedQuestion = question2.ClosedQuestion,
                        ManyCorrectAnswers = question2.ManyCorrectAnswers,
                        Photo = question2.Photo?.Image,
                        PointsForQuestion = question2.PointsForQuestion,
                        PossibleAnswers = question2.Answers?.Select(a => a.Value)
                    },
                    new QuestionView
                    {
                        Title = question3.Title,
                        ClosedQuestion = question3.ClosedQuestion,
                        ManyCorrectAnswers = question3.ManyCorrectAnswers,
                        Photo = question3.Photo?.Image,
                        PointsForQuestion = question3.PointsForQuestion,
                        PossibleAnswers = question3.Answers?.Select(a => a.Value)
                    }
                }
            };
        }
    }
}
