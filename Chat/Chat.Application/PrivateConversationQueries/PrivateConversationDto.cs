using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Chat.Application.PrivateConversationQueries
{
    public class PrivateConversationDto : IRequest<QueryResult<List<MessageView>>>
    {
        public string SenderId { get; set; } = string.Empty;
        public string RecipientId { get; set; } = string.Empty;
    }
}
