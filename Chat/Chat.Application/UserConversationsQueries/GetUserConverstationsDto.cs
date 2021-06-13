using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Chat.Application.UserConversationsQueries
{
    public class GetUserConverstationsDto : IRequest<QueryResult<List<UserConversationsView>>>
    {
        public string UserEmail { get; set; } = string.Empty;
    }
}
