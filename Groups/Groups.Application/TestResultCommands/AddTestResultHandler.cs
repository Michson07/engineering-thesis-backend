using Core.Api;
using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestResultCommands
{
    public class AddTestResultHandler : IRequestHandler<AddTestResultDto, CommandResult>
    {
        private readonly ITestAggregateRepository testRepository;
        private readonly ITestResultAggregateRepository testResultRepository;

        public AddTestResultHandler(ITestAggregateRepository testRepository, ITestResultAggregateRepository testResultRepository)
        {
            this.testRepository = testRepository;
            this.testResultRepository = testResultRepository;
        }

        public async Task<CommandResult> Handle(AddTestResultDto request, CancellationToken cancellationToken)
        {
            var test = await testRepository.GetTestById(request.TestId);
            if(test == null)
            {
                return new CommandResult { Result = new NotFoundResult<string>(request.TestId) };
            }

            var userInGroup = test.Group.Participients.FirstOrDefault(p => p.Email == request.StudentEmail);
            if(userInGroup == null)
            {
                return new CommandResult { Result = new NotAllowedResult<string, object>(request.StudentEmail, new { Test = test.Id }) };
            }

            var studentAnswers = MapToUserAnswers(request.StudentAnswers, test);
            var testResult = TestResultAggregate.Create(test, userInGroup, studentAnswers);
            testResultRepository.Add(testResult);

            await testResultRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }

        private IEnumerable<StudentAnswer> MapToUserAnswers(IEnumerable<StudentAnswerDto> dto, TestAggregate test)
        {
            var studentAnswers = new List<StudentAnswer>();

            foreach(var studentAnswer in dto)
            {
                var question = test.Questions.FirstOrDefault(question => question.Title == studentAnswer.Question);
                if(question == null)
                {
                    throw new Exception($"Question {studentAnswer.Question} not found");
                }

                studentAnswers.Add(new StudentAnswer(question, studentAnswer.ReceivedAnswers));
            }

            return studentAnswers;
        }
    }
}
