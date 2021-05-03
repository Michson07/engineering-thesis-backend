using Core.Api;
using Core.Application;
using Core.Domain.ValueObjects;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.GroupsCommands
{
    public class AddGroupHandler : IRequestHandler<AddGroupDto, CommandResult>
    {
        private readonly IGroupAggregateRepository repository;

        public AddGroupHandler(IGroupAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AddGroupDto request, CancellationToken cancellationToken)
        {
            var groupExists = repository.Get(request.Name);

            if (groupExists != null)
            {
                return new CommandResult { Result = new ConflictResult<string>(request.Name) };
            }

            var owner = new Participient(new Email(request.OwnerEmail), GroupRoles.Owner);
            var group = (GroupAggregate.Create(new List<Participient>() { owner },
                new GroupName(request.Name),
                request.Description));

            repository.Add(group);
            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
