using Core.Application;
using Groups.Database.ResourceAggregateDatabase;
using MediatR;
using System;
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

            if (resource == null)
            {
                throw new Exception($"Nie znaleziono materiału o id {request.Id}");
            }
            if (resource.File == null)
            {
                throw new Exception($"Materiał nie zawiera pliku");
            }

            return new QueryResult<ResourceValueView> { BodyResponse = new ResourceValueView { Value = resource.File.Value } };
        }
    }
}
