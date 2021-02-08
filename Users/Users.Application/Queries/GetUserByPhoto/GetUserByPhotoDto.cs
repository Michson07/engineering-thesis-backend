using Core.Application;
using MediatR;

namespace Users.Application.Queries.GetUserByPhoto
{
    public class GetUserByPhotoDto : IRequest<QueryResult<GetUserByPhotoView>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
