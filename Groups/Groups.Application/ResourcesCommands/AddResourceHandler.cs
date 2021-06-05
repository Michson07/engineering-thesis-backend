using Core.Api;
using Core.Application;
using Core.Domain.ValueObjects;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.ResourceAggregateDatabase;
using Groups.Domain.Aggregates;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.ResourcesCommands
{
    public class AddResourceHandler : IRequestHandler<AddResourceDto, CommandResult>
    {
        private readonly IResourceAggregateRepository resourceRepository;
        private readonly IGroupAggregateRepository groupRepository;

        public AddResourceHandler(IResourceAggregateRepository resourceRepository, IGroupAggregateRepository groupRepository)
        {
            this.resourceRepository = resourceRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<CommandResult> Handle(AddResourceDto request, CancellationToken cancellationToken)
        {
            var group = groupRepository.GetById(request.GroupId);
            if (group == null)
            {
                return new CommandResult { Result = new NotFoundResult<string>(request.GroupId) };
            }

            var file = new File(request.FileName, request.Type, request.Value);
            var resource = ResourceAggregate.Create(request.Name, file, group);
            await resourceRepository.Add(resource);    
            await resourceRepository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
