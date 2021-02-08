using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Application.Queries.GetUserByPhoto
{
    public class GetUserByPhotoHandler : IRequestHandler<GetUserByPhotoDto, QueryResult<GetUserByPhotoView>>
    {
        public Task<QueryResult<GetUserByPhotoView>> Handle(GetUserByPhotoDto request, CancellationToken cancellationToken)
        {
            var response = new QueryResult<GetUserByPhotoView>
            {
                Body = new GetUserByPhotoView { Photo = 1 }
            };

            return Task.FromResult(response);
        }
    }
}
