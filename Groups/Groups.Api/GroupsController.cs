using Core.Api;
using Groups.Application.GroupsCommands;
using Groups.Application.GroupsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Groups.Api
{
    [Route("api/group")]
    public class GroupsController : BaseController
    {
        private readonly IMediator mediator;

        public GroupsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddGroupAsync(AddGroupDto group)
        {
            var response = await mediator.Send(group);

            return response.Result;
        }

        [HttpGet]
        public ApiActionResult GetUserGroups(string email)
        {
            var response = mediator.Send(new GetUserGroupsDto { Email = email });

            return response.Result;
        }
    }
}
