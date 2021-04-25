using Core.Api;
using Groups.Application.GroupsCommands;
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
        public ActionResult<ApiActionResult> AddUser(AddGroupDto group)
        {
            var response = mediator.Send(group);

            return response.Result.Result;
        }
    }
}
