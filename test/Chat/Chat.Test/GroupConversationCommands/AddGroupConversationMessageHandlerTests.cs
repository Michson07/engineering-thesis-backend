using Chat.Application.GroupConversationCommands;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Application.Test.GroupConversationCommands
{
    public class AddGroupConversationMessageHandlerTests : ChatServicesMock
    {
        private readonly string groupId = Guid.NewGuid().ToString();
        private readonly AddGroupConversationMessageHandler handler;
        private readonly Message firstMessage = new Message(new Email("test@test.com"), "test");

        public AddGroupConversationMessageHandlerTests()
        {
            groupChatRepository.Add(GroupChatAggregate.Create(groupId, firstMessage));

            handler = new AddGroupConversationMessageHandler(groupChatRepository);
        }

        [Fact]
        public async Task ShouldAddNewMessageForExistingChat()
        {
            var newMessageDto = new AddGroupConversationMessageDto
            {
                GroupId = groupId,
                Message = "new Message",
                SenderEmail = "test@test.com"
            };

            var result = await handler.Handle(newMessageDto, CancellationToken.None);
            var chat = await groupChatRepository.Get(groupId);
            var messages = chat.Messages;

            Assert.Equal("Ok", result.Result.Body);
            Assert.True(messages.Count == 2);
            Assert.Equal(firstMessage, messages.First());
            Assert.Equal("new Message", messages.Last().Text);
            Assert.Equal("test@test.com", messages.Last().UserEmail);
        }

        [Fact]
        public async Task ShouldCreateGroupChatAndAddNewMessageForNonExistingChat()
        {
            var newGroupId = "B3AD9BDC-3B4C-42ED-A59A-AFFEB87E7D00";
            var newMessageDto = new AddGroupConversationMessageDto
            {
                GroupId = newGroupId,
                Message = "first Message",
                SenderEmail = "test@test.com"
            };

            var result = await handler.Handle(newMessageDto, CancellationToken.None);
            var chat = await groupChatRepository.Get(newGroupId);
            var messages = chat.Messages;

            Assert.Equal("Ok", result.Result.Body);
            Assert.True(messages.Count == 1);
            Assert.Equal("first Message", messages.First().Text);
            Assert.Equal("test@test.com", messages.First().UserEmail);
        }
    }
}
