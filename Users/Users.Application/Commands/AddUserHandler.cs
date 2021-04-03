using Core.Application;
using Core.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;
using Users.Domain.Aggregates;

namespace Users.Application.Commands
{
    public class AddUserHandler : IRequestHandler<AddUserDto, CommandResult>
    {
        private readonly IUserAggregateRepository repository;

        public AddUserHandler(IUserAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<CommandResult> Handle(AddUserDto request, CancellationToken cancellationToken)
        {
            var userAggregate = UserAggregate.Create(Email.Create(request.Email), request.Name, request.LastName, null);
            repository.Add(userAggregate);
            repository.SaveChanges();

            return Task.FromResult(new CommandResult());
        }
    }
}
