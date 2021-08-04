using FluentAssertions;
using Groups.Application.TestQueries;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestQueries
{
    public class GetTestsInTimePeriodHandlerTest : GroupsServicesMock
    {
        private readonly GetTestsInTimePeriodHandler handler;

        public GetTestsInTimePeriodHandlerTest()
        {
            handler = new GetTestsInTimePeriodHandler(testAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetTomorrowTestsAsync()
        {
            var today = DateTime.Today;
            var test1 = new TestAggregateBuilder().WithDate(today).Build();
            var test2 = new TestAggregateBuilder()
                .WithName("Test 2")
                .WithTimeDuration(20)
                .WithPassedFrom(30)
                .WithRequirePhoto(true)
                .WithDate(new DateTime(today.Year, today.Month, today.Day, 10, 0, 0)).Build();
            var test3 = new TestAggregateBuilder()
                .WithDate(today.AddDays(7))
                .Build();

            await testAggregateRepository.Add(test1);
            await testAggregateRepository.Add(test2);
            await testAggregateRepository.Add(test3);

            var response = await handler.Handle(new() { Time = today.AddDays(-1) }, CancellationToken.None);
            response.BodyResponse.Should().BeEquivalentTo(Expected(test1, test2));
        }

        private static IEnumerable<TestInTimePeriodView> Expected(params TestAggregate[] tests)
        {
            var test1 = tests.First();
            var test2 = tests.ElementAt(1);

            return new List<TestInTimePeriodView>
            {
                new()
                {
                    TestDate = test1.Date,
                    TestId = test1.Id.ToString(),
                    TestName = test1.Name,
                    TestRequirePhoto = test1.RequirePhoto,
                    TestTimeDuration = test1.TimeDuration,
                    Emails = test1.Group.Participients.Select(user => user.Email.ToString())
                },
                new()
                {
                    TestDate = test2.Date,
                    TestId = test2.Id.ToString(),
                    TestName = test2.Name,
                    TestRequirePhoto = test2.RequirePhoto,
                    TestTimeDuration = test2.TimeDuration,
                    Emails = test2.Group.Participients.Select(user => user.Email.ToString())
                }
            };
        }
    }
}
