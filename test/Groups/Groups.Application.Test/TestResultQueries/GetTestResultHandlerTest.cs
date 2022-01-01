using FluentAssertions;
using Groups.Application.TestResultQueries;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestResultQueries
{
    public class GetTestResultHandlerTest : GroupsServicesMock
    {
        private readonly GetTestResultHandler handler;

        public GetTestResultHandlerTest()
        {
            handler = new GetTestResultHandler(testResultAggregateRepository, testAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetTestResultAsync()
        {
            var test = new TestAggregateBuilder().Build();

            var testResult = new TestResultAggregateBuilder()
                .WithTest(test)
                .Build();

            await testAggregateRepository.Add(test);
            testResultAggregateRepository.Add(testResult);

            var request = new GetTestResultDto
            {
                Email = testResult.Student.Email,
                TestId = test.Id.ToString()
            };

            var response = await handler.Handle(request, CancellationToken.None);
            var expected = ExpectedView(testResult);

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldCreateResultForAbsentUserAsync()
        {
            var student = new Participient(new("student@mail.com"), GroupRoles.Student);

            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient>()
                {
                    new(new("owner@mail.com"), GroupRoles.Owner),
                    student,
                })
                .Build();

            var test = new TestAggregateBuilder()
                .WithDate(DateTime.MinValue)
                .WithGroup(group)
                .Build();

            var testResult = new TestResultAggregateBuilder()
                .WithTest(test)
                .WithStudentAnswers(Array.Empty<StudentAnswer>())
                .WithStudent(student)
                .Build();

            await testAggregateRepository.Add(test);
            groupAggregateRepository.Add(group);

            var request = new GetTestResultDto
            {
                Email = testResult.Student.Email,
                TestId = test.Id.ToString()
            };

            var response = await handler.Handle(request, CancellationToken.None);
            var expected = ExpectedViewForAbsent(testResult);

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }

        private TestResultView ExpectedViewForAbsent(TestResultAggregate testResult)
        {
            return new()
            {
                State = testResult.State,
                Checked = testResult.Checked == CheckedState.Checked,
                TestName = testResult.Test.Name,
                PassedFrom = testResult.Test.PassedFrom,
                ReceivedPoints = testResult.ReceivedPoints,
                InfoAboutPoints = "Nie wszystkie odpowiedzi zostały sprawdzone",
                Questions = new List<QuestionView>()
                {
                    new()
                    {
                        Question = "Question 1",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 1").PointsForQuestion,
                        ReceivedPoints = 0,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 1").Answers.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = new List<string> { "" }
                    },
                    new()
                    {
                        Question = "Question 2",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 2").PointsForQuestion,
                        ReceivedPoints = 0,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 2").Answers.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = new List<string> { "" }
                    },
                    new()
                    {
                        Question = "Question 3",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 3").PointsForQuestion,
                        ReceivedPoints = 0,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 3").Answers?.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = new List<string> { "" }
                    }
                }
            };
        }

        private TestResultView ExpectedView(TestResultAggregate testResult)
        {
            return new()
            {
                State = testResult.State,
                Checked = testResult.Checked == CheckedState.Checked,
                TestName = testResult.Test.Name,
                PassedFrom = testResult.Test.PassedFrom,
                ReceivedPoints = testResult.ReceivedPoints,
                InfoAboutPoints = "Test się jeszcze nie skończył",
                Questions = new List<QuestionView>()
                {
                    new()
                    {
                        Question = "Question 1",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 1").PointsForQuestion,
                        ReceivedPoints = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 1").PointsForAnswer,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 1").Answers.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 1").ReceivedAnswers
                    },
                    new()
                    {
                        Question = "Question 2",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 2").PointsForQuestion,
                        ReceivedPoints = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 2").PointsForAnswer,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 2").Answers.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 2").ReceivedAnswers
                    },
                    new()
                    {
                        Question = "Question 3",
                        PossiblePoints = testResult.Test.Questions.Single(q => q.Title == "Question 3").PointsForQuestion,
                        ReceivedPoints = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 3").PointsForAnswer,
                        CorrectAnswers = testResult.Test.Questions.Single(q => q.Title == "Question 3").Answers?.Where(a => a.Correct).Select(a => a.Value),
                        UserAnswers = testResult.StudentAnswers.Single(q => q.Question.Title == "Question 3").ReceivedAnswers
                    }
                }
            };
        }
    }
}
