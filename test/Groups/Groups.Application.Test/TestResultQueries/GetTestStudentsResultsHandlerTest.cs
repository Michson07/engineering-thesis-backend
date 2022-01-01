using Core.Application;
using FluentAssertions;
using Groups.Application.TestResultQueries;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;
using Xunit;

namespace Groups.Application.Test.TestResultQueries
{
    public class GetTestStudentsResultsHandlerTest : GroupsServicesMock
    {
        private readonly GetTestStudentsResultsHandler handler;

        public GetTestStudentsResultsHandlerTest()
        {
            handler = new GetTestStudentsResultsHandler(testResultAggregateRepository, testAggregateRepository, mediator);
        }

        [Fact]
        public async Task ShouldReturnTestResultsAsync()
        {
            var test = new TestAggregateBuilder().Build();

            var testResult1 = new TestResultAggregateBuilder()
                .WithTest(test)
                .Build();
            var testResult2 = new TestResultAggregateBuilder()
                .WithStudent(new(new("student2@mail.com"), GroupRoles.Student))
                .WithTest(test)
                .Build();

            await testAggregateRepository.Add(test);
            testResultAggregateRepository.Add(testResult1);
            testResultAggregateRepository.Add(testResult2);

            MockGetUserHandler(testResult1.Student.Email);
            MockGetUserHandler(testResult2.Student.Email);

            var request = new GetTestStudentsResultsDto
            {
                Email = testResult1.Test.Group.Participients.Single().Email,
                TestId = test.Id.ToString()
            };

            var response = await handler.Handle(request, CancellationToken.None);
            var expected = ExpectedView(new(testResult1, "Jan", "Nowak"), new(testResult2, "Jan", "Nowak"));

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldNotAllowToGetResultsByNotGroupOwnerAsync()
        {
            var test = new TestAggregateBuilder().Build();

            await testAggregateRepository.Add(test);

            var request = new GetTestStudentsResultsDto
            {
                Email = "notOwner@mail.com",
                TestId = test.Id.ToString()
            };

            var ex = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.Equal("Użytkownik nie jest właścicielem grupy", ex.Message);
        }

        private void MockGetUserHandler(string email)
        {
            mediator.Send(Arg.Is<GetUserByEmailDto>(user => user.Email == email))
                .Returns(Task.FromResult(new QueryResult<UserView>
                {
                    BodyResponse = new()
                    {
                        Email = email,
                        Name = "Jan",
                        LastName = "Nowak",
                    }
                }
            ));
        }

        private List<TestStudentsResultsView> ExpectedView(params (TestResultAggregate, string, string)[] results)
        {
            var result1 = results.First();
            var result2 = results.Last();

            return new()
            {
                new()
                {
                    Email = result1.Item1.Student.Email,
                    Name = result1.Item2,
                    LastName = result1.Item3,
                    Result = new()
                    {
                        State = result1.Item1.State,
                        Checked = result1.Item1.Checked == CheckedState.Checked,
                        TestName = result1.Item1.Test.Name,
                        PassedFrom = result1.Item1.Test.PassedFrom,
                        ReceivedPoints = result1.Item1.ReceivedPoints,
                        InfoAboutPoints = "Test się jeszcze nie skończył",
                        Questions = new List<QuestionView>()
                        {
                            new()
                            {
                                Question = "Question 1",
                                PossiblePoints = result1.Item1.Test.Questions.Single(q => q.Title == "Question 1").PointsForQuestion,
                                ReceivedPoints = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 1").PointsForAnswer,
                                CorrectAnswers = result1.Item1.Test.Questions.Single(q => q.Title == "Question 1").Answers.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 1").ReceivedAnswers
                            },
                            new()
                            {
                                Question = "Question 2",
                                PossiblePoints = result1.Item1.Test.Questions.Single(q => q.Title == "Question 2").PointsForQuestion,
                                ReceivedPoints = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 2").PointsForAnswer,
                                CorrectAnswers = result1.Item1.Test.Questions.Single(q => q.Title == "Question 2").Answers.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 2").ReceivedAnswers
                            },
                            new()
                            {
                                Question = "Question 3",
                                PossiblePoints = result1.Item1.Test.Questions.Single(q => q.Title == "Question 3").PointsForQuestion,
                                ReceivedPoints = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 3").PointsForAnswer,
                                CorrectAnswers = result1.Item1.Test.Questions.Single(q => q.Title == "Question 3").Answers?.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result1.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 3").ReceivedAnswers
                            }
                        }
                    }
                },
                new()
                {
                    Email = result2.Item1.Student.Email,
                    Name = result2.Item2,
                    LastName = result2.Item3,
                    Result = new()
                    {
                        State = result2.Item1.State,
                        Checked = result2.Item1.Checked == CheckedState.Checked,
                        TestName = result2.Item1.Test.Name,
                        PassedFrom = result2.Item1.Test.PassedFrom,
                        ReceivedPoints = result2.Item1.ReceivedPoints,
                        InfoAboutPoints = "Test się jeszcze nie skończył",
                        Questions = new List<QuestionView>()
                        {
                            new()
                            {
                                Question = "Question 1",
                                PossiblePoints = result2.Item1.Test.Questions.Single(q => q.Title == "Question 1").PointsForQuestion,
                                ReceivedPoints = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 1").PointsForAnswer,
                                CorrectAnswers = result2.Item1.Test.Questions.Single(q => q.Title == "Question 1").Answers.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 1").ReceivedAnswers
                            },
                            new()
                            {
                                Question = "Question 2",
                                PossiblePoints = result2.Item1.Test.Questions.Single(q => q.Title == "Question 2").PointsForQuestion,
                                ReceivedPoints = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 2").PointsForAnswer,
                                CorrectAnswers = result2.Item1.Test.Questions.Single(q => q.Title == "Question 2").Answers.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 2").ReceivedAnswers
                            },
                            new()
                            {
                                Question = "Question 3",
                                PossiblePoints = result2.Item1.Test.Questions.Single(q => q.Title == "Question 3").PointsForQuestion,
                                ReceivedPoints = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 3").PointsForAnswer,
                                CorrectAnswers = result2.Item1.Test.Questions.Single(q => q.Title == "Question 3").Answers?.Where(a => a.Correct).Select(a => a.Value),
                                UserAnswers = result2.Item1.StudentAnswers.Single(q => q.Question.Title == "Question 3").ReceivedAnswers
                            }
                        }
                    }
                }
            };
        }
    }
}
