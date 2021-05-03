using Core.Api;
using Core.Application;
using Core.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserDto, CommandResult>
    {
        private readonly IUserAggregateRepository repository;
        private readonly IMediator mediator;

        public UpdateUserHandler(IUserAggregateRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<CommandResult> Handle(UpdateUserDto request, CancellationToken cancellationToken)
        {
            var user = repository.Get(request.Email);
            if (user == null)
            {
                return await mediator.Send(new AddUserDto
                {
                    Email = request.Email,
                    Name = request.Name,
                    LastName = request.LastName
                }, cancellationToken);
            }

            var photo = request.Photo != null ? new Photo(Convert.FromBase64String(request.Photo)) : null;

            user.Update(new Email(request.Email), request.Name, request.LastName, photo);
            repository.Update(user);
            await repository.SaveChanges();

            return new CommandResult { Result = new OkResult() };
        }
    }
}