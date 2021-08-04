using Core.Application;
using Groups.Application.GroupsQueries;
using Groups.Domain.Test.Aggregates;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;
using Xunit;

namespace Groups.Application.Test.GroupsQueries
{
    public class GetGroupByNameHandlerTest : GroupsServicesMock
    {
        private readonly GetGroupByNameHandler handler;

        public GetGroupByNameHandlerTest()
        {
            handler = new GetGroupByNameHandler(groupAggregateRepository, mediator);
        }

        [Fact]
        public async Task ShouldFindGroupAsync()
        {
            mediator.Send(Arg.Any<GetUserByEmailDto>())
                .Returns(Task.FromResult(new QueryResult<UserView>
                {
                    BodyResponse = new()
                    {
                        Email = "creator@mail.com",
                        Name = "Jan",
                        LastName = "Nowak",
                    }
                }
            ));

            var group = new GroupAggregateBuilder()
                .WithGroupName("Group")
                .WithDescription("Group - description")
                .WithIsOpen(true)
                .Build();

            groupAggregateRepository.Add(group);

            var request = new GetGroupByNameDto { Name = group.GroupName };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(group.Id.ToString(), response.BodyResponse.Id);
            Assert.Equal(group.GroupName, response.BodyResponse.Name);
            Assert.Equal("Jan Nowak", response.BodyResponse.Owner);
            Assert.Empty(response.BodyResponse.Error);
        }

        [Fact]
        public async Task ShouldReturnErrorWhenGroupWithNameDoesNotExistsAsync()
        {
            var request = new GetGroupByNameDto { Name = "notExisting" };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Empty(response.BodyResponse.Id);
            Assert.Empty(response.BodyResponse.Name);
            Assert.Empty(response.BodyResponse.Owner);
            Assert.Equal("Grupa z podaną nazwą nie istnieje", response.BodyResponse.Error);
        }

        [Fact]
        public async Task ShouldReturnErrorWhenGroupRequiresCode()
        {
            var group = new GroupAggregateBuilder()
                .WithIsOpen(false)
                .Build();

            groupAggregateRepository.Add(group);

            var request = new GetGroupByNameDto { Name = group.GroupName };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Empty(response.BodyResponse.Id);
            Assert.Empty(response.BodyResponse.Name);
            Assert.Empty(response.BodyResponse.Owner);
            Assert.Equal("Dołączenie do tej grupy wymaga podania kluczu dostępu", response.BodyResponse.Error);
        }
    }
}
