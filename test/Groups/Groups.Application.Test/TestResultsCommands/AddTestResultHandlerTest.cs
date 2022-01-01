using Core.Api;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using Groups.Application.TestResultCommands;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestResultsCommands
{
    public class AddTestResultHandlerTest : GroupsServicesMock
    {
        private readonly AddTestResultHandler handler;

        public AddTestResultHandlerTest()
        {
            handler = new AddTestResultHandler(testAggregateRepository, testResultAggregateRepository);
        }

        [Fact]
        public async Task ShouldAddTestResultAsync()
        {
            var userEmail = new Email("student@mail.com");
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> {
                    new(new(userEmail), GroupRoles.Student),
                    new(new("creator@mail.com"), GroupRoles.Owner)
                }
            ).Build();

            var test = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            await testAggregateRepository.Add(test);

            var request = new AddTestResultDto()
            {
                TestId = test.Id.ToString(),
                StudentEmail = userEmail,
                StudentAnswers = new()
                {
                    new() { Question = test.Questions.First().Title, ReceivedAnswers = new() { "Correct" } },
                    new() { Question = test.Questions.ElementAt(1).Title, ReceivedAnswers = new() { "8" } },
                    new() { Question = test.Questions.Last().Title, ReceivedAnswers = new() { "Australia" } },
                }
            };

            var response = await handler.Handle(request, CancellationToken.None);
            var testResult = await testResultAggregateRepository.GetTestResult(userEmail, test.Id.ToString());

            Assert.IsType<OkResult>(response.Result);
            Assert.Equal("NotChecked", testResult.Checked);
            Assert.Equal(2, testResult.ReceivedPoints);
            Assert.Equal(3, testResult.StudentAnswers.Count());
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenTestNotFound()
        {
            var request = new AddTestResultDto() { TestId = "notExisting" };

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Nie znaleziono testu o id: notExisting", ex.Message);
        }

        [Fact]
        public async Task ShouldReturnNotAllowedWhenUserIsNotInGroup()
        {
            var userEmail = new Email("student@mail.com");
            var group = new GroupAggregateBuilder().Build();

            var test = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            await testAggregateRepository.Add(test);

            var request = new AddTestResultDto() { TestId = test.Id.ToString(), StudentEmail = userEmail };
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<NotAllowedResult<string, object>>(response.Result);
            Assert.Equal($"\"student@mail.com\" not allowed to act with {JsonConvert.SerializeObject(new { Test = test.Id })}.", response.Result.Body);
        }
    }
}
