using Core.Application;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.TestAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.GroupsQueries
{
    public class GetUserGroupsHandler : IRequestHandler<GetUserGroupsDto, QueryResult<List<GroupView>>>
    {
        private readonly IGroupAggregateRepository groupRepository;
        private readonly ITestAggregateRepository testRepository;

        public GetUserGroupsHandler(IGroupAggregateRepository groupRepository, ITestAggregateRepository testRepository)
        {
            this.groupRepository = groupRepository;
            this.testRepository = testRepository;
        }

        public async Task<QueryResult<List<GroupView>>> Handle(GetUserGroupsDto request, CancellationToken cancellationToken)
        {
            var groups = await groupRepository.GetUserGroups(request.Email);
            var groupsView = groups.Select(g => new GroupView 
            {
                Id = g.Id.ToString(), 
                Name = g.GroupName, 
                Description = g.Description,
                Code = g.Code,
                IsOwner = g.Participients.First(p => p.Email == request.Email).Role == GroupRoles.Owner
            }).ToList();

            foreach(var groupView in groupsView)
            {
                var tests = testRepository.GetGroupTests(groupView.Name);
                groupView.Tests = MapTestsToView(tests);
            }

            return new QueryResult<List<GroupView>> { BodyResponse = groupsView };
        }

        private IEnumerable<TestGroupView> MapTestsToView(IEnumerable<TestAggregate>? tests)
        {
            var views = new List<TestGroupView>();
            if(tests != null)
            {
                foreach (var test in tests)
                {
                    views.Add(new TestGroupView { Id = test.Id.ToString(), Name = test.Name, Date = test.Date });
                }
            }
            
            return views;
        }
    }
}
