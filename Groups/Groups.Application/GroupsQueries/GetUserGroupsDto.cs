using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.GroupsQueries
{
    public class GetUserGroupsDto : IRequest<QueryResult<List<GroupView>>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
