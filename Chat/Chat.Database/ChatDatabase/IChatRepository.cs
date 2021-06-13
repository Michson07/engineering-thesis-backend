using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Database.ChatDatabase
{
    public interface IChatRepository
    {
        Task<IEnumerable<ConversationBasic>> Get(string userEmail, IEnumerable<Guid> groupsIds);
    }

    public class ConversationBasic
    {
        public Guid EntityId { get; init; }
        public bool EntityIsGroup { get; init; }
        public bool EntityIsPrivate { get; init; }
        public string LastMessage { get; init; } = string.Empty;
        public DateTime LastMessageDate { get; init; }

        public ConversationBasic(
            Guid entityId, 
            bool entityIsGroup,
            bool entityIsPrivate,
            string lastMessage, 
            DateTime lastMessageDate)
        {
            EntityId = entityId;
            EntityIsGroup = entityIsGroup;
            EntityIsPrivate = entityIsPrivate;
            LastMessage = lastMessage;
            LastMessageDate = lastMessageDate;
        }
    }
}
