using FluentAssertions;
using Groups.Application.TestQueries;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.TestQueries
{
    public class GetUserTestsHandlerTest : GroupsServicesMock
    {
        private readonly GetUserTestsHandler handler;

        public GetUserTestsHandlerTest()
        {
            handler = new(testAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetUserTestsAsync()
        {
            var userEmail = "user@mail.com";

            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> {
                    new(new(userEmail), GroupRoles.Student),
                    new(new("creator@mail.com"), GroupRoles.Owner)
                }
            ).Build();

            var test1 = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            var test2 = new TestAggregateBuilder()
                .WithGroup(group)
                .WithName("Test 2")
                .WithDate(test1.Date.AddDays(2))
                .Build();

            await testAggregateRepository.Add(test1);
            await testAggregateRepository.Add(test2);

            var response = await handler.Handle(new() { Email = userEmail }, CancellationToken.None);

            response.BodyResponse.Should().BeEquivalentTo(Expected(test1, test2));
        }

        private static IEnumerable<TestBasicView> Expected(params TestAggregate[] tests)
        {
            var test1 = tests.First();
            var test2 = tests.Last();

            return new List<TestBasicView>
            {
                new()
                {
                    Date = test1.Date,
                    Name = test1.Name,
                    TimeDuration = test1.TimeDuration,
                    PassedFrom = test1.PassedFrom,
                    RequirePhoto = test1.RequirePhoto
                },
                new()
                {
                    Date = test2.Date,
                    Name = test2.Name,
                    TimeDuration = test2.TimeDuration,
                    PassedFrom = test2.PassedFrom,
                    RequirePhoto = test2.RequirePhoto
                }
            };
        }
    }
}
