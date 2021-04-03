using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Queries.GetUserByPhoto
{
    public class GetUserByPhotoHandler : IRequestHandler<GetUserByPhotoDto, QueryResult<GetUserByPhotoView>>
    {
        private readonly IUserAggregateRepository repository;

        public GetUserByPhotoHandler(IUserAggregateRepository repository)
        {
            this.repository = repository;
        }

        //todo change to proper logic
        public Task<QueryResult<GetUserByPhotoView>> Handle(GetUserByPhotoDto request, CancellationToken cancellationToken)
        {
            var user = repository.Get(request.Email);

            var response = new QueryResult<GetUserByPhotoView>();
            if(user == null)
            {
                response.Body = null;
            }
            else
            {
                response.Body = new GetUserByPhotoView
                {
                    Email = user.Email.EmailAddress,
                    Name = user.Name,
                    LastName = user.LastName,
                    Photo = user.Photo?.Image
                };
            }

            return Task.FromResult(response);
        }
    }
}
