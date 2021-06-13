using System;

namespace Chat.Application.UserConversationsQueries
{
    public class UserConversationsView
    {
        public string? GroupId { get; set; }
        public string? RecipientEmail { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastMessage { get; init; } = string.Empty;
        public DateTime LastMessageDate { get; init; }
    }
}
