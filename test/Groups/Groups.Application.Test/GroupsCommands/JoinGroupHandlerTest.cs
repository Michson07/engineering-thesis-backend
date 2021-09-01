using Core.Api;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using Groups.Application.GroupsCommands;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
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

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Nie znaleziono grupy o id: notExistingGroupId", ex.Message);
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

            var ex = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal($"{request.Email} już należy do grupy {group.GroupName}", ex.Message);
        }
    }
}
