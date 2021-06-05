using Core.Application;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.ResourceAggregateDatabase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.ResourcesQueries
{
    public class GetResourceValueHandler : IRequestHandler<GetResourceValueDto, QueryResult<ResourceValueView>>
    {
        private readonly IResourceAggregateRepository resourceRepository;

        public GetResourceValueHandler(IResourceAggregateRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        public async Task<QueryResult<ResourceValueView>> Handle(GetResourceValueDto request, CancellationToken cancellationToken)
        {
            var resource = await resourceRepository.GetResourceById(request.Id);

            if(resource == null)
            {
                throw new Exception($"Nie znaleziono materiału o id {request.Id}");
            }

            return new QueryResult<ResourceValueView> { BodyResponse = new ResourceValueView { Value = resource.File.Value } };
        }
    }
}
