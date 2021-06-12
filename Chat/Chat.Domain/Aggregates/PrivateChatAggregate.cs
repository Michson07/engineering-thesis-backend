using Chat.Domain.ValueObjects;
using Core.Domain;
using System;
using System.Collections.Generic;

namespace Chat.Domain.Aggregates
{
    public class PrivateChatAggregate : AggregateRoot
    {
        public Guid User1Id { get; private set; }
        public Guid User2Id { get; private set; }
        public ICollection<Message> Messages { get; private set; }

        private PrivateChatAggregate()
        {
        }

        private PrivateChatAggregate(string user1Id, string user2Id, Message message)
        {
            User1Id = new Guid(user1Id);
            User2Id = new Guid(user2Id);
            Messages = new List<Message>() { message };
        }

        public static PrivateChatAggregate Create(string user1Id, string user2Id, Message message)
        {
            return new PrivateChatAggregate(user1Id, user2Id, message);
        }

        public PrivateChatAggregate Update(Message message)
        {
            Messages.Add(message);

            return this;
        }
    }
}
