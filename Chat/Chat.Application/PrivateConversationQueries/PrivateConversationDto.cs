using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Chat.Application.PrivateConversationQueries
{
    public class PrivateConversationDto : IRequest<QueryResult<List<MessageView>>>
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
    }
}
