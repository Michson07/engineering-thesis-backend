using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestQueries
{
    public class GetTestHandler : IRequestHandler<GetTestDto, QueryResult<TestView>>
    {
        private readonly ITestAggregateRepository testRepository;

        public GetTestHandler(ITestAggregateRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<QueryResult<TestView>> Handle(GetTestDto request, CancellationToken cancellationToken)
        {
            var test = await testRepository.GetTestById(request.TestId);

            if (test == null)
            {
                return new QueryResult<TestView> { BodyResponse = null };
            }

            var testView = MapToTestView(test);

            return new QueryResult<TestView> { BodyResponse = testView };
        }

        private TestView MapToTestView(TestAggregate test)
        {
            var testView = new TestView
            {
                Date = test.Date,
                Name = test.Name,
                RequirePhoto = test.RequirePhoto,
                PassedFrom = test.PassedFrom,
                TimeDuration = test.TimeDuration
            };

            var questionsView = new List<QuestionView>();
            foreach (var question in test.Questions)
            {
                questionsView.Add(new QuestionView
                {
                    Title = question.Title,
                    Photo = question.Photo?.Image,
                    ClosedQuestion = question.ClosedQuestion,
                    PointsForQuestion = question.PointsForQuestion,
                    ManyCorrectAnswers = question.ManyCorrectAnswers,
                    PossibleAnswers = question.Answers?.Select(a => a.Value)
                });
            }

            testView.Questions = questionsView;

            return testView;
        }
    }
}
