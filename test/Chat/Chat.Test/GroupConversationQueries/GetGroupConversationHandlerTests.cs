using Chat.Application.GroupConversationQueries;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Application.Test.GroupConversationQueries
{
    public class GetGroupConversationHandlerTests : ChatServicesMock
    {
        private readonly string groupId = Guid.NewGuid().ToString();
        private readonly GetGroupConversationHandler handler;

        public GetGroupConversationHandlerTests()
        {
            var message = new Message(new Email("test@test.com"), "test");
            groupChatRepository.Add(GroupChatAggregate.Create(groupId, message));

            handler = new GetGroupConversationHandler(groupChatRepository);
        }

        [Fact]
        public async Task ShouldGetGroupConversationAsync()
        {
            var expectedMessageView = new MessageView
            {
                Text = "test",
                User = "test@test.com",
                Date = DateTime.UtcNow
            };

            var result = await handler.Handle(new GroupConversationDto { GroupId = groupId }, CancellationToken.None);
            var messages = result.BodyResponse;
            var message = messages.First();

            Assert.True(messages.Count == 1);
            Assert.Equal(expectedMessageView.Text, message.Text);
            Assert.Equal(expectedMessageView.User, message.User);
        }
    }
}
