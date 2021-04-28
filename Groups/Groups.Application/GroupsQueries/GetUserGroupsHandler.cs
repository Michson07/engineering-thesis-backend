using Core.Application;
using Groups.Database.GroupsAggregateDatabase;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.GroupsQueries
{
    public class GetUserGroupsHandler : IRequestHandler<GetUserGroupsDto, QueryResult<List<GroupView>>>
    {
        private readonly IGroupAggregateRepository repository;

        public GetUserGroupsHandler(IGroupAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<QueryResult<List<GroupView>>> Handle(GetUserGroupsDto request, CancellationToken cancellationToken)
        {
            var groups = repository.GetUserGroups(request.Email);
            var groupsNames = groups.Select(g => new GroupView { Name = g.GroupName, Description = g.Description }).ToList();
            var response = new QueryResult<List<GroupView>> { BodyResponse = groupsNames };

            return Task.FromResult(response);
        }
    }
}
