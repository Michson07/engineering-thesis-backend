using Core.Api;
using Core.Domain.ValueObjects;
using Groups.Application.TestResultCommands;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using Notifications.Application.NotificationCommands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestResultsCommands
{
    public class UpdateTestResultStatusHandlerTest : GroupsServicesMock
    {
        private readonly UpdateTestResultStatusHandler handler;

        public UpdateTestResultStatusHandlerTest()
        {
            handler = new(testResultAggregateRepository, testAggregateRepository, mediator);
        }

        [Fact]
        public async Task ShouldUpdateTestResultStatus()
        {
            var studentEmail = new Email("student@mail.com");
            var teacherEmail = new Email("teacher@mail.com");

            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient>()
                {
                    new (teacherEmail, GroupRoles.Owner),
                    new (studentEmail, GroupRoles.Student)
                }).Build();

            var test = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            var testResult = new TestResultAggregateBuilder()
                .WithTest(test)
                .WithStudent(new(studentEmail, GroupRoles.Student))
                .Build();

            await testAggregateRepository.Add(test);
            testResultAggregateRepository.Add(testResult);

            var request = new UpdateTestResultStatusDto
            {
                Email = teacherEmail,
                UserEmail = studentEmail,
                TestId = test.Id.ToString(),
                Questions = new List<QuestionResultDto>
                {
                    new()
                    {
                        Question = test.Questions.First().Title,
                        ReceivedPoints = 2
                    },
                    new()
                    {
                        Question = test.Questions.ElementAt(1).Title,
                        ReceivedPoints = 1
                    },
                    new()
                    {
                        Question = test.Questions.Last().Title,
                        ReceivedPoints = 3
                    }
                }
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<OkResult>(response.Result);
            Assert.Equal("Checked", testResult.Checked);
            Assert.Equal(6, testResult.ReceivedPoints);
            await mediator.Received(1).Send(Arg.Any<SendEmailNotificationDto>());
        }

        [Fact]
        public async Task ShouldFailWhenTeacherIsNotGroupOwnerAsync()
        {
            var request = new UpdateTestResultStatusDto { Email = "notOwner" };
            var test = new TestAggregateBuilder()
                .Build();

            await testAggregateRepository.Add(test);
            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Użytkownik nie jest właścielem grupy", ex.Message);
        }

        [Fact]
        public async Task ShouldFailWhenTestResultDoesNotExists()
        {
            var studentEmail = new Email("student@mail.com");
            var teacherEmail = new Email("teacher@mail.com");

            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient>()
                {
                    new (teacherEmail, GroupRoles.Owner)
                }).Build();

            var test = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            await testAggregateRepository.Add(test);

            var request = new UpdateTestResultStatusDto
            {
                Email = teacherEmail,
                UserEmail = studentEmail,
                TestId = test.Id.ToString()
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Test z podanym użytkownikiem nie istnieje", ex.Message);
        }
    }
}
