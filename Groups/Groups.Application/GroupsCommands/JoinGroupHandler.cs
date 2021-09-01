using Core.Api;
using Core.Application;
using Core.Application.Exceptions;
using Core.Domain.ValueObjects;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain;
using Groups.Domain.ValueObjects;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.GroupsCommands
{
    public class JoinGroupHandler : IRequestHandler<JoinGroupDto, CommandResult>
    {
        private readonly IGroupAggregateRepository repository;

        public JoinGroupHandler(IGroupAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<CommandResult> Handle(JoinGroupDto request, CancellationToken cancellationToken)
        {
            var group = repository.GetById(request.Id);
            if (group == null)
            {
                throw new NotFoundException(request.Id, "grupy");
            }

            if (group.Participients.Any(p => p.Email == request.Email))
            {
                throw new DomainException($"{request.Email} już należy do grupy {group.GroupName}");
            }

            var updatedParticipients = group.Participients.ToList();
            updatedParticipients.Add(new Participient(new Email(request.Email), GroupRoles.Student));

            var updatedGroup = group.Update(updatedParticipients, group.GroupName, group.Description, group.Code == null);
            repository.Update(updatedGroup);
            repository.SaveChanges();

            return Task.FromResult(new CommandResult { Result = new OkResult() });
        }
    }
}
