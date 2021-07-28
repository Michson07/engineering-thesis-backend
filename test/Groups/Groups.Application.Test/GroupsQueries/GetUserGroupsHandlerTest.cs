using Core.Domain.ValueObjects;
using FluentAssertions;
using Groups.Application.GroupsQueries;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.GroupsQueries
{
    public class GetUserGroupsHandlerTest : GroupsServicesMock
    {
        private readonly GetUserGroupsHandler handler;

        public GetUserGroupsHandlerTest()
        {
            handler = new GetUserGroupsHandler(groupAggregateRepository, testAggregateRepository);
        }

        [Fact]
        public async Task ShouldReturnUserGroupsAsync()
        {
            var email = new Email("user@mail.com");
            var participient = new Participient(email, GroupRoles.Owner);

            var group1 = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> { participient })
                .Build();

            var group2 = new GroupAggregateBuilder()
                .WithGroupName("Group2")
                .WithDescription("Group2 - description")
                .WithParticipients(new List<Participient> { participient })
                .Build();

            var group3 = new GroupAggregateBuilder()
                .Build();

            var test = new TestAggregateBuilder().WithGroup(group1).Build();

            groupAggregateRepository.Add(group1);
            groupAggregateRepository.Add(group2);
            groupAggregateRepository.Add(group3);
            await testAggregateRepository.Add(test);

            var request = new GetUserGroupsDto { Email = email };

            var response = await handler.Handle(request, CancellationToken.None);

            var expected = new List<GroupView>
            {
                new()
                {
                    Id = group1.Id.ToString(),
                    Name = group1.GroupName,
                    Description = group1.Description,
                    IsOwner = true,
                    Tests = new List<TestGroupView> { new() { Id = test.Id.ToString(), Name = test.Name, Date = test.Date } }
                },
                new()
                {
                    Id = group2.Id.ToString(),
                    Name = group2.GroupName,
                    Description = group2.Description,
                    IsOwner = true,
                    Tests = new List<TestGroupView>()
                }
            };

            response.BodyResponse.Should().BeEquivalentTo(expected);
        }
    }
}
