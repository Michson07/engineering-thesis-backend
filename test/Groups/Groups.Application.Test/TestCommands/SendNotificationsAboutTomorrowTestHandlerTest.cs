using Core.Application;
using Groups.Application.TestCommands;
using Groups.Application.TestQueries;
using Notifications.Application.NotificationCommands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestCommands
{
    public class SendNotificationsAboutTomorrowTestHandlerTest : GroupsServicesMock
    {
        private readonly SendNotificationsAboutTomorrowTestHandler handler;

        public SendNotificationsAboutTomorrowTestHandlerTest()
        {
            handler = new SendNotificationsAboutTomorrowTestHandler(mediator);
        }

        [Fact]
        public async Task ShouldSendNotifications()
        {
            mediator.Send(Arg.Any<GetTestsInTimePeriodDto>())
                .Returns(Task.FromResult(new QueryResult<List<TestInTimePeriodView>>
                {
                    BodyResponse = new List<TestInTimePeriodView>
                    {
                        new TestInTimePeriodView
                        {
                            TestId = "abcd",
                            Emails = new List<string> { "a@a.com", "b@b.com" },
                            TestDate = new DateTime(2021, 8, 20, 15, 0, 0),
                            TestName = "Test 1",
                            TestRequirePhoto = false,
                            TestTimeDuration = 45
                        },
                        new TestInTimePeriodView
                        {
                            TestId = "efgh",
                            Emails = new List<string> { "c@c.com", "b@b.com" },
                            TestDate = new DateTime(2021, 8, 20, 20, 0, 0),
                            TestName = "Test 2",
                            TestRequirePhoto = true,
                            TestTimeDuration = 120
                        }
                    }
                })
            );

            await handler.Handle(new(), CancellationToken.None);
            await mediator.Received(2).Send(Arg.Any<CreateEventNotificationsDto>());
        }
    }
}
