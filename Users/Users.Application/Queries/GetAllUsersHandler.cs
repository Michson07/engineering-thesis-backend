using Core.Application;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersDto, QueryResult<List<UserView>>>
    {
        private readonly IUserAggregateRepository repository;

        public GetAllUsersHandler(IUserAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<QueryResult<List<UserView>>> Handle(GetAllUsersDto request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllUsersAsync();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                usersView.Add(new UserView
                {
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName,
                    Photo = user.Photo?.Image
                });
            }

            return new QueryResult<List<UserView>> { BodyResponse = usersView };
        }
    }
}
