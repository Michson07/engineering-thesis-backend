using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Queries
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailDto, QueryResult<GetUserByEmailView>>
    {
        private readonly IUserAggregateRepository repository;

        public GetUserByEmailHandler(IUserAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<QueryResult<GetUserByEmailView>> Handle(GetUserByEmailDto request, CancellationToken cancellationToken)
        {
            var user = repository.Get(request.Email);
            var userView = user != null ?
                new GetUserByEmailView
                {
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName
                } : null;

            return Task.FromResult(new QueryResult<GetUserByEmailView> { BodyResponse = userView });
        }
    }
}
