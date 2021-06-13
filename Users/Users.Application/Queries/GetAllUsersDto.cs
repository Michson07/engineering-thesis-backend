using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Users.Application.Queries
{
    public class GetAllUsersDto : IRequest<QueryResult<List<UserView>>>
    {
    }
}
