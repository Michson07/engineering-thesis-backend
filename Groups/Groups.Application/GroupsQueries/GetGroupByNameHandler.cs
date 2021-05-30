using Core.Application;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;

namespace Groups.Application.GroupsQueries
{
    public class GetGroupByNameHandler : IRequestHandler<GetGroupByNameDto, QueryResult<GroupBasicView>>
    {
        private readonly IGroupAggregateRepository repository;
        private readonly IMediator mediator;

        public GetGroupByNameHandler(IGroupAggregateRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<QueryResult<GroupBasicView>> Handle(GetGroupByNameDto request, CancellationToken cancellationToken)
        {
            var group = await repository.GetByName(request.Name);
            if (group == null)
            {
                return new QueryResult<GroupBasicView>
                {
                    BodyResponse = new GroupBasicView { Error = "Grupa z podaną nazwą nie istnieje" }
                };
            }

            if (group.Code != null)
            {
                return new QueryResult<GroupBasicView>
                {
                    BodyResponse = new GroupBasicView { Error = "Dołączenie do tej grupy wymaga podania kluczu dostępu" }
                };
            }

            var groupOwnerEmail = group.Participients.First(g => g.Role == GroupRoles.Owner).Email;
            var groupOwner = mediator.Send(new GetUserByEmailDto { Email = groupOwnerEmail }).Result.BodyResponse;
            var groupOwnerNameLastName = groupOwner!.Name + " " + groupOwner!.LastName;

            return new QueryResult<GroupBasicView>
            {
                BodyResponse = new GroupBasicView { Id = group.Id.ToString(), Name = group.GroupName, Owner = groupOwnerNameLastName }
            };
        }
    }
}