using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.ResourcesQueries
{
    public class GetResourcesDto : IRequest<QueryResult<List<ResourceView>>>
    {
        public string GroupId { get; set; } = string.Empty;
    }
}
