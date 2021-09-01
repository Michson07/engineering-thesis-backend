using Core.Api;
using Core.Application.Exceptions;
using Groups.Application.TestCommands;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestCommands
{
    public class AddTestHandlerTest : GroupsServicesMock
    {
        private readonly AddTestHandler handler;

        public AddTestHandlerTest()
        {
            handler = new AddTestHandler(groupAggregateRepository, testAggregateRepository, questionAggregateRepository);
        }

        [Fact]
        public async Task ShouldAddTestAsync()
        {
            var group = new GroupAggregateBuilder().Build();
            groupAggregateRepository.Add(group);

            var question1 = new QuestionAggregateBuilder()
                .WithClosedQuestion(true)
                .WithAnswers(new List<Answer> { new("A", true), new("B", false) })
                .Build();

            var question2 = new QuestionAggregateBuilder()
                .WithTitle("Question nr 2")
                .WithClosedQuestion(false)
                .WithAnswers(Array.Empty<Answer>())
                .Build();

            var request = new AddTestDto
            {
                Date = new DateTime(2021, 07, 29, 15, 30, 0),
                Group = group.Id.ToString(),
                Name = "Test 1",
                PassedFrom = 50,
                RequirePhoto = true,
                TimeDuration = 45,
                Questions = new List<QuestionDto>
                {
                    new QuestionDto
                    {
                        Title = question1.Title,
                        ClosedQuestion = question1.ClosedQuestion,
                        Photo = question1.Photo?.Image,
                        Points = question1.PointsForAnswer,
                        Answers = new List<AnswerDto>
                        {
                            new AnswerDto
                            {
                                Value = question1.Answers.First().Value,
                                Correct = question1.Answers.First().Correct
                            },
                            new AnswerDto
                            {
                                Value = question1.Answers.Last().Value,
                                Correct = question1.Answers.Last().Correct
                            }
                        }
                    },
                    new QuestionDto
                    {
                        Title = question2.Title,
                        ClosedQuestion = question2.ClosedQuestion,
                        Photo = question2.Photo?.Image,
                        Points = question2.PointsForAnswer,
                        Answers = Array.Empty<AnswerDto>()
                    }
                }
            };

            var response = await handler.Handle(request, CancellationToken.None);

            var test = testAggregateRepository.GetGroupTests(group.GroupName).Single();
            var testQuestion1 = test.Questions.First();
            var testQuestion2 = test.Questions.Last();

            Assert.IsType<OkResult>(response.Result);
            Assert.Equal("Test 1", test.Name);
            Assert.Equal(new DateTime(2021, 07, 29, 17, 30, 0), test.Date);
            Assert.Equal(50, test.PassedFrom);
            Assert.True(test.RequirePhoto);
            Assert.Equal(45, test.TimeDuration);

            Assert.Equal(question1.Title, testQuestion1.Title);
            Assert.Equal(question1.ClosedQuestion, testQuestion1.ClosedQuestion);
            Assert.Equal(question1.ManyCorrectAnswers, testQuestion1.ManyCorrectAnswers);
            Assert.Equal(question1.Photo, testQuestion1.Photo);
            Assert.Equal(question1.PointsForQuestion, testQuestion1.PointsForQuestion);
            Assert.Equal(2, testQuestion1.Answers.Count());

            Assert.Equal(question2.Title, testQuestion2.Title);
            Assert.Equal(question2.ClosedQuestion, testQuestion2.ClosedQuestion);
            Assert.Equal(question2.ManyCorrectAnswers, testQuestion2.ManyCorrectAnswers);
            Assert.Equal(question2.Photo, testQuestion2.Photo);
            Assert.Equal(question2.PointsForQuestion, testQuestion2.PointsForQuestion);
            Assert.Null(testQuestion2.Answers);
        }

        [Fact]
        public async Task ShouldNotAllowToCreateTestForNotExistingGroupAsync()
        {
            var request = new AddTestDto { Group = "notExistingGroup" };

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Nie znaleziono grupy o id: notExistingGroup", ex.Message);
        }
    }
}
