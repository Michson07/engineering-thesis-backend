using Core.Application;
using MediatR;

namespace Groups.Application.GroupsQueries
{
    public class GetGroupByNameDto : IRequest<QueryResult<GroupBasicView>>
    {
        public string Name { get; set; } = string.Empty;
    }
}
