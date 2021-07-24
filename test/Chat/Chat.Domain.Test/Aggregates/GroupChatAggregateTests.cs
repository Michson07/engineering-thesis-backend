using Chat.Domain.Aggregates;
using Chat.Domain.ValueObjects;
using Core.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace Chat.Domain.Test.Aggregates
{
    public class GroupChatAggregateTests
    {
        private readonly Message firstMessage = new Message(new Email("user@mail.com"), "message");

        [Fact]
        public void ShouldCreateGroupChatAggregate()
        {
            var chat = GroupChatAggregate.Create("253A0BE7-2A25-4F4F-A1DE-02F9498CA68C", firstMessage);

            Assert.Equal(new Guid("253A0BE7-2A25-4F4F-A1DE-02F9498CA68C"), chat.GroupId);
            Assert.True(chat.Messages.Count == 1);
            Assert.Equal(firstMessage, chat.Messages.First());
        }

        [Fact]
        public void ShouldAddMessageToGroupChatAggregate()
        {
            var chat = GroupChatAggregate.Create("253A0BE7-2A25-4F4F-A1DE-02F9498CA68C", firstMessage);

            var secondMessage = new Message(new Email("user@mail.com"), "second message");
            var thirdMessage = new Message(new Email("user2@mail.com"), "third message");
            chat.Update(secondMessage);
            chat.Update(thirdMessage);

            Assert.Equal(new Guid("253A0BE7-2A25-4F4F-A1DE-02F9498CA68C"), chat.GroupId);
            Assert.True(chat.Messages.Count == 3);
            Assert.Equal(firstMessage, chat.Messages.First());
            Assert.Equal(thirdMessage, chat.Messages.Last());
        }
    }
}
