using Core.Api;
using Groups.Application.GroupsCommands;
using Groups.Application.GroupsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public ApiActionResult AddGroup(AddGroupDto group)
        {
            var response = mediator.Send(group);

            return response.Result.Result;
        }

        [HttpGet]
        public ApiActionResult GetUserGroups(string email)
        {
            var response = mediator.Send(new GetUserGroupsDto { Email = email });

            return response.Result;
        }
    }
}
