using Core.Application;
using MediatR;

namespace Groups.Application.GroupsQueries
{
    public class GetGroupByCodeDto : IRequest<QueryResult<GroupBasicView>>
    {
        public string Code { get; set; } = string.Empty;
    }
}
