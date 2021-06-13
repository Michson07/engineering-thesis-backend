using Core.Application;
using MediatR;

namespace Users.Application.Queries
{
    public class GetUserByEmailDto : IRequest<QueryResult<UserView>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
