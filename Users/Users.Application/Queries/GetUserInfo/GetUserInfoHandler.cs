using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Application.Queries.GetUserInfo
{
    public class GetUserInfoHandler : IRequestHandler<GetUserInfoDto, QueryResult<GetUserInfoView>>
    {
        public Task<QueryResult<GetUserInfoView>> Handle(GetUserInfoDto request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
