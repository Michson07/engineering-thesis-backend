using Core.Api;
using Core.Domain.ValueObjects;
using Groups.Application.GroupsCommands;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Application.Test.GroupsCommands
{
    public class JoinGroupHandlerTest : GroupsServicesMock
    {
        private readonly JoinGroupHandler handler;

        public JoinGroupHandlerTest()
        {
            handler = new JoinGroupHandler(groupAggregateRepository);
        }

        [Fact]
        public async Task ShouldAllowToJoinToGroupAsync()
        {
            var group = new GroupAggregateBuilder().Build();
            var email = new Email("student@mail.com");

            groupAggregateRepository.Add(group);

            var request = new JoinGroupDto
            {
                Id = group.Id.ToString(),
                Email = email
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<OkResult>(response.Result);
            Assert.Contains(new(email, GroupRoles.Student), group.Participients);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenGroupDoesNotExists()
        {
            var request = new JoinGroupDto
            {
                Id = "notExistingGroupId"
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<NotFoundResult<string>>(response.Result);
            Assert.Equal("notExistingGroupId not found.", response.Result.Body);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUserAlreadyIsInTheGroup()
        {
            var email = new Email("student@mail.com");
            var group = new GroupAggregateBuilder()
                .WithParticipients(new List<Participient> { new(email, GroupRoles.Owner) })
                .Build();

            groupAggregateRepository.Add(group);

            var request = new JoinGroupDto
            {
                Id = group.Id.ToString(),
                Email = email
            };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.IsType<ConflictResult<string>>(response.Result);
            Assert.Equal("student@mail.com already exists.", response.Result.Body);
        }
    }
}
