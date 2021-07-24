using Chat.Application.PrivateConversationCommands;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Application.Test.PrivateConversationCommands
{
    public class AddPrivateConversationMessageHandlerTests : ChatServicesMock
    {
        private readonly AddPrivateConversationMessageHandler handler;
        private readonly Message firstMessage;
        private readonly Email user1Email = new Email("user1@test.com");
        private readonly Email user2Email = new Email("user2@test.com");

        public AddPrivateConversationMessageHandlerTests()
        {
            firstMessage = new Message(user1Email, "first Message");
            privateChatAggregateRepository.Add(PrivateChatAggregate.Create(user1Email, user2Email, firstMessage));

            handler = new AddPrivateConversationMessageHandler(privateChatAggregateRepository);
        }

        [Fact]
        public async Task ShouldAddMessageToExisitingChat()
        {
            var request = new AddPrivateConversationMessageDto
            {
                Message = "second Message",
                SenderEmail = user2Email,
                RecipientEmail = user1Email
            };

            var result = await handler.Handle(request, CancellationToken.None);
            var chat = await privateChatAggregateRepository.Get(user1Email, user2Email);
            var messages = chat.Messages;

            Assert.Equal("Ok", result.Result.Body);
            Assert.True(messages.Count == 2);
            Assert.Equal(firstMessage, messages.First());
            Assert.Equal("second Message", messages.Last().Text);
            Assert.Equal("user2@test.com", messages.Last().UserEmail);
        }

        [Fact]
        public async Task ShouldCreateChatAndAddMessageForNonExistingChat()
        {
            var user3Eamil = new Email("user3@test.com");
            var request = new AddPrivateConversationMessageDto
            {
                Message = "second Message",
                SenderEmail = user1Email,
                RecipientEmail = user3Eamil
            };

            var result = await handler.Handle(request, CancellationToken.None);
            var chat = await privateChatAggregateRepository.Get(user1Email, user3Eamil);
            var messages = chat.Messages;

            Assert.Equal("Ok", result.Result.Body);
            Assert.True(messages.Count == 1);
            Assert.Equal("second Message", messages.First().Text);
            Assert.Equal("user1@test.com", messages.First().UserEmail);
            Assert.Equal(user3Eamil, chat.User2Email);
        }
    }
}
