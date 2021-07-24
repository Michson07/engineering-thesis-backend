using Chat.Application.UserConversationsQueries;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Application;
using Core.Domain.ValueObjects;
using Groups.Application.GroupsQueries;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;
using Xunit;

namespace Chat.Application.Test.UserConversationsQueries
{
    public class GetUserConversationsHandlerTests : ChatServicesMock
    {
        private readonly Email userEmail = new Email("user@test.com");
        private readonly GetUserConversationsHandler handler;
        private readonly string userGroup = "AD89A960-6E13-4901-A585-02E7BC9FFA5A";
        private readonly Email user2Email = new Email("user2@test.com");

        public GetUserConversationsHandlerTests()
        {
            var firstGroupChatMessage = new Message(new Email("group@test.com"), "first group message");
            groupChatRepository.Add(GroupChatAggregate.Create(userGroup, firstGroupChatMessage));

            var firstPrivateChatMessage = new Message(new Email(user2Email), "first private message");
            privateChatAggregateRepository.Add(PrivateChatAggregate.Create(userEmail, user2Email, firstPrivateChatMessage));

            handler = new GetUserConversationsHandler(chatRepository, mediator, privateChatAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetGroupAndPrivateChatsForUserAsync()
        {
            GetUserGroupsMock();
            GetUserByEmailMock(userEmail);

            var result = await handler.Handle(new GetUserConverstationsDto { UserEmail = userEmail }, CancellationToken.None);
            var conversationsViews = result.BodyResponse;

            Assert.True(conversationsViews.Count == 2);
        }

        private void GetUserGroupsMock()
        {
            mediator.Send(Arg.Any<GetUserGroupsDto>())
                .Returns(Task.FromResult(new QueryResult<List<GroupView>>
                {
                    BodyResponse = new List<GroupView>
                    {
                        new GroupView
                        {
                            Id = userGroup,
                            Description = "Description",
                            IsOwner = false,
                            Name = "Group1",
                        }
                    }
                }));
        }
        private void GetUserByEmailMock(string email)
        {
            mediator.Send(Arg.Any<GetUserByEmailDto>())
                .Returns(Task.FromResult(new QueryResult<UserView>
                {
                    BodyResponse = new UserView
                    {
                        Email = email,
                        Name = "Jan",
                        LastName = "Nowak",
                        Photo = null
                    }
                }));
        }
    }
}
