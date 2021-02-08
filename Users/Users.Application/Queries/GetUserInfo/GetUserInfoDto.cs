using Core.Application;
using MediatR;

namespace Users.Application.Queries.GetUserInfo
{
    public class GetUserInfoDto : IRequest<QueryResult<GetUserInfoView>>
    {
        public string Id { get; set; }
    }
}
