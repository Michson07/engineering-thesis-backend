using Core.Api;
using Core.Application;
using Core.Application.Exceptions;
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

        public async Task<CommandResult> Handle(AddUserDto request, CancellationToken cancellationToken)
        {
            var userExists = repository.Get(request.Email);
            if(userExists != null)
            {
                throw new DomainException($"Użytkownik z mailem {request.Email} już istnieje");
            }

            var userAggregate = UserAggregate.Create(new Email(request.Email), request.Name, request.LastName, null);
            repository.Add(userAggregate);
            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}
