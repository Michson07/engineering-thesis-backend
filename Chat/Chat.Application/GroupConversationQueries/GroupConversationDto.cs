using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Chat.Application.GroupConversationQueries
{
    public class GroupConversationDto : IRequest<QueryResult<List<MessageView>>>
    {
        public string GroupId { get; set; } = string.Empty;
    }
}
