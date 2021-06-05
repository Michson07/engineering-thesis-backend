using Core.Application;
using MediatR;

namespace Groups.Application.ResourcesQueries
{
    public class GetResourceValueDto : IRequest<QueryResult<ResourceValueView>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
