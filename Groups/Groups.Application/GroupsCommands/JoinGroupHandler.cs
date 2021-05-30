using Core.Api;
using Core.Application;
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
                return Task.FromResult(new CommandResult { Result = new NotFoundResult<string>(request.Id) });
            }

            if (group.Participients.Any(p => p.Email == request.Email))
            {
                return Task.FromResult(new CommandResult { Result = new ConflictResult<string>(request.Email) });
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
