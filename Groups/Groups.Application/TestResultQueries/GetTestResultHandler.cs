using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestResultQueries
{
    public class GetTestResultHandler : IRequestHandler<GetTestResultDto, QueryResult<TestResultView>>
    {
        private readonly ITestResultAggregateRepository repository;
        private readonly ITestAggregateRepository testRepository;

        public GetTestResultHandler(ITestResultAggregateRepository repository, ITestAggregateRepository testRepository)
        {
            this.repository = repository;
            this.testRepository = testRepository;
        }
        //todo add trigger to set points 0 to all users when test finished and no answers (in new handler)
        public async Task<QueryResult<TestResultView>> Handle(GetTestResultDto request, CancellationToken cancellationToken)
        {
            var userResult = await repository.GetTestResult(request.Email, request.TestId);
            if(userResult == null)
            {
                var test = await testRepository.GetTestById(request.TestId);
                
                if(test != null && test.Date.AddMinutes(test.TimeDuration) < DateTime.Now)
                {
                    userResult = TestResultAggregate.CreateForAbsent(test, test.Group.Participients.First(p => p.Email == request.Email));
                    repository.Add(userResult);
                } 
                else
                {
                    return new QueryResult<TestResultView> { BodyResponse = null };
                }
            }

            var response = new TestResultView
            {
                State = userResult.State,
                ReceivedPoints = userResult.ReceivedPoints,
                InfoAboutPoints = GetStatus(userResult),
                Questions = MapToQuestionsView(userResult)
            };

            await repository.SaveChanges();

            return new QueryResult<TestResultView> { BodyResponse = response };
        }

        private string? GetStatus(TestResultAggregate testResult)
        {
            var testIsFinished = testResult.Test.Date.AddMinutes(testResult.Test.TimeDuration) < DateTime.Now;

            if (!testIsFinished)
            {
                return "Test się jeszcze nie skończył";
            }

            if(testResult.Checked == CheckedState.NotChecked)
            {
                return "Nie wszystkie odpowiedzi zostały sprawdzone";
            }

            return null;
        }

        private IEnumerable<QuestionView> MapToQuestionsView(TestResultAggregate testResult)
        {
            var questionsView = new List<QuestionView>();

            foreach(var question in testResult.StudentAnswers)
            {
                questionsView.Add(new QuestionView
                {
                    Question = question.Question.Title,
                    ReceivedPoints = question.PointsForAnswer,
                    PossiblePoints = question.Question.PointsForQuestion,
                    UserAnswers = question.ReceivedAnswers,
                    CorrectAnswers = question.Question.Answers?.Where(a => a.Correct).Select(a => a.Value)
                });
            }

            return questionsView;
        }
    }
}
