using Core.Api;
using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestResultCommands
{
    public class UpdateTestResultStatusHandler : IRequestHandler<UpdateTestResultStatusDto, CommandResult>
    {
        private readonly ITestResultAggregateRepository testResultRepository;
        private readonly ITestAggregateRepository testRepository;

        public UpdateTestResultStatusHandler(ITestResultAggregateRepository testResultRepository, ITestAggregateRepository testRepository)
        {
            this.testResultRepository = testResultRepository;
            this.testRepository = testRepository;
        }

        public async Task<CommandResult> Handle(UpdateTestResultStatusDto request, CancellationToken cancellationToken)
        {
            var isOwner = await testRepository.UserIsAllowedToCheckResults(request.TestId, request.Email);
            if (!isOwner)
            {
                throw new Exception("User is not an owner of a group!");
            }

            var testResult = await testResultRepository.GetTestResult(request.UserEmail, request.TestId);
            if (testResult == null)
            {
                throw new Exception("Test with user does not exists");
            }

            var newPoints = request.Questions.Select(q => (q.Question, q.ReceivedPoints));
            var changedPoints = ChangeAnswersPoints(testResult.StudentAnswers, newPoints);
            var updated = testResult.Update(testResult.Test, testResult.Student, changedPoints);

            testResultRepository.Update(updated);
            await testResultRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }

        private IEnumerable<StudentAnswer> ChangeAnswersPoints(IEnumerable<StudentAnswer> currentAnswers, IEnumerable<(string, int)> newPoints)
        {
            var newStudentAnswers = new List<StudentAnswer>();
            foreach (var answer in currentAnswers)
            {
                var points = newPoints.First(q => q.Item1 == answer.Question.Title).Item2;
                newStudentAnswers.Add(new StudentAnswer(answer.Question, answer.ReceivedAnswers, points));
            }

            return newStudentAnswers;
        }
    }
}
