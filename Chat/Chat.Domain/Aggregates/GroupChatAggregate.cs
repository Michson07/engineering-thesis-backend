using Chat.Domain.ValueObjects;
using Core.Domain;
using System;
using System.Collections.Generic;

namespace Chat.Domain.Aggregates
{
    public class GroupChatAggregate : AggregateRoot
    {
        public Guid GroupId { get; private set; }
        public ICollection<Message> Messages { get; private set; }

        private GroupChatAggregate()
        {
        }

        private GroupChatAggregate(string groupId, Message message)
        {
            GroupId = new Guid(groupId);
            Messages = new List<Message>() { message };
        }

        public static GroupChatAggregate Create(string groupId, Message message)
        {
            return new GroupChatAggregate(groupId, message);
        }

        public GroupChatAggregate Update(Message message)
        {
            Messages.Add(message);

            return this;
        }
    }
}
