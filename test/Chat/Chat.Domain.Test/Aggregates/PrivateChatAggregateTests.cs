using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System.Linq;
using Xunit;

namespace Chat.Domain.Test.Aggregates
{
    public class PrivateChatAggregateTests
    {
        private readonly Message firstMessage = new Message(new Email("user@mail.com"), "message");
        private readonly Email firstUserEmail = new Email("user1@mail.com");
        private readonly Email secondUserEmail = new Email("user2@mail.com");

        [Fact]
        public void ShouldCreateGroupChatAggregate()
        {
            var chat = PrivateChatAggregate.Create(firstUserEmail, secondUserEmail, firstMessage);

            Assert.Equal(firstUserEmail, chat.User1Email);
            Assert.Equal(secondUserEmail, chat.User2Email);
            Assert.True(chat.Messages.Count == 1);
            Assert.Equal(firstMessage, chat.Messages.First());
        }

        [Fact]
        public void ShouldAddMessageToGroupChatAggregate()
        {
            var chat = PrivateChatAggregate.Create(firstUserEmail, secondUserEmail, firstMessage);

            var secondMessage = new Message(secondUserEmail, "second message");
            var thirdMessage = new Message(secondUserEmail, "third message");
            chat.Update(secondMessage);
            chat.Update(thirdMessage);

            Assert.Equal(firstUserEmail, chat.User1Email);
            Assert.Equal(secondUserEmail, chat.User2Email);
            Assert.True(chat.Messages.Count == 3);
            Assert.Equal(firstMessage, chat.Messages.First());
            Assert.Equal(thirdMessage, chat.Messages.Last());
        }
    }
}
