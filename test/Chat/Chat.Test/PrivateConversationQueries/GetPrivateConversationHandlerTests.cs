using Chat.Application.PrivateConversationQueries;
using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chat.Application.Test.PrivateConversationQueries
{
    public class GetPrivateConversationHandlerTests : ChatServicesMock
    {
        private readonly GetPrivateConversationHandler handler;
        private readonly Message firstMessage;
        private readonly Email user1Email = new Email("user1@test.com");
        private readonly Email user2Email = new Email("user2@test.com");

        public GetPrivateConversationHandlerTests()
        {
            firstMessage = new Message(user1Email, "first Message");
            privateChatAggregateRepository.Add(PrivateChatAggregate.Create(user1Email, user2Email, firstMessage));

            handler = new GetPrivateConversationHandler(privateChatAggregateRepository);
        }

        [Fact]
        public async Task ShouldGetPrivateConversation()
        {
            var dto = new PrivateConversationDto
            {
                RecipientEmail = user1Email,
                SenderEmail = user2Email,
            };

            var result = await handler.Handle(dto, CancellationToken.None);
            var messages = result.BodyResponse;
            var message = messages.First();

            Assert.True(messages.Count == 1);
            Assert.Equal("first Message", message.Text);
            Assert.Equal("first Message", message.Text);
        }

        [Fact]
        public async Task ShouldGetEmptyPrivateConversation()
        {
            var dto = new PrivateConversationDto
            {
                RecipientEmail = user1Email,
                SenderEmail = "user3@test.com",
            };

            var result = await handler.Handle(dto, CancellationToken.None);
            var messages = result.BodyResponse;

            Assert.Empty(messages);
        }
    }
}
