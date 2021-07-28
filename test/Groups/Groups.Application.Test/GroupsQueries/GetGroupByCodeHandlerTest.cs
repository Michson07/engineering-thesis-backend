using Core.Application;
using Groups.Application.GroupsQueries;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using NSubstitute;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;
using Xunit;

namespace Groups.Application.Test.GroupsQueries
{
    public class GetGroupByCodeHandlerTest : GroupsServicesMock
    {
        private readonly GetGroupByCodeHandler handler;

        public GetGroupByCodeHandlerTest()
        {
            handler = new GetGroupByCodeHandler(groupAggregateRepository, mediator);
        }

        [Fact]
        public async Task ShouldReturnGroupWhenCodeIsCorrectAsync()
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
                .WithIsOpen(false)
                .Build();

            groupAggregateRepository.Add(group);
            
            var request = new GetGroupByCodeDto { Code = group.Code };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(group.Id.ToString(), response.BodyResponse.Id);
            Assert.Equal(group.GroupName, response.BodyResponse.Name);
            Assert.Equal("Jan Nowak", response.BodyResponse.Owner);
            Assert.Empty(response.BodyResponse.Error);
        }

        [Fact]
        public async Task ShouldReturnErrorWhenGroupWithCodeDoesNotExistsAsync()
        {
            var request = new GetGroupByCodeDto { Code = "notExisting" };

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Empty(response.BodyResponse.Id);
            Assert.Empty(response.BodyResponse.Name);
            Assert.Empty(response.BodyResponse.Owner);
            Assert.Equal("Grupa z podanym kodem nie istnieje", response.BodyResponse.Error);
        }
    }
}
