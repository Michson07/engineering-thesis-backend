using Chat.Domain.ValueObjects;
using Core.Domain;
using Core.Domain.ValueObjects;
using System.Collections.Generic;

namespace Chat.Domain.Aggregates
{
    public class PrivateChatAggregate : AggregateRoot
    {
        public Email User1Email { get; private set; }
        public Email User2Email { get; private set; }
        public ICollection<Message> Messages { get; private set; }

        private PrivateChatAggregate()
        {
        }

        private PrivateChatAggregate(Email user1Email, Email user2Email, Message message)
        {
            User1Email = user1Email;
            User2Email = user2Email;
            Messages = new List<Message>() { message };
        }

        public static PrivateChatAggregate Create(Email user1Email, Email user2Email, Message message)
        {
            return new PrivateChatAggregate(user1Email, user2Email, message);
        }

        public PrivateChatAggregate Update(Message message)
        {
            Messages.Add(message);

            return this;
        }
    }
}
