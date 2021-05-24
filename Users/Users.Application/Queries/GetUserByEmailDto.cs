using Core.Application;
using MediatR;

namespace Users.Application.Queries
{
    public class GetUserByEmailDto : IRequest<QueryResult<GetUserByEmailView>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
