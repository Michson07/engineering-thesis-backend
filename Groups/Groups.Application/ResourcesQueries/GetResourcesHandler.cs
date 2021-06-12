using Core.Application;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.ResourceAggregateDatabase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.ResourcesQueries
{
    public class GetResourcesHandler : IRequestHandler<GetResourcesDto, QueryResult<List<ResourceView>>>
    {
        private readonly IResourceAggregateRepository resourceRepository;
        private readonly IGroupAggregateRepository groupRepository;

        public GetResourcesHandler(IResourceAggregateRepository resourceRepository, IGroupAggregateRepository groupRepository)
        {
            this.resourceRepository = resourceRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<QueryResult<List<ResourceView>>> Handle(GetResourcesDto request, CancellationToken cancellationToken)
        {
            var group = groupRepository.GetById(request.GroupId);
            if (group == null)
            {
                throw new Exception($"Grupa o id {request.GroupId} nie istnieje");
            }

            var resources = await resourceRepository.GetGroupResources(request.GroupId);
            return new QueryResult<List<ResourceView>> { BodyResponse = MapToView(resources).ToList() };
        }

        private IEnumerable<ResourceView> MapToView(IEnumerable<ResourceAggregateWithoutFile> resources)
        {
            var resourcesView = new List<ResourceView>();

            foreach (var resource in resources)
            {
                resourcesView.Add(new ResourceView
                {
                    Id = resource.Id.ToString(),
                    Name = resource.Name,
                    FileName = resource.FileName,
                    Type = resource.FileType,
                    Date = resource.AddedDate
                });
            }

            return resourcesView;
        }
    }
}
