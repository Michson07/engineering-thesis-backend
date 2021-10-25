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
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"get {email} groups");
            }

            var response = mediator.Send(new GetUserGroupsDto { Email = email });

            return response.Result;
        }

        [Route("findByCode")]
        [HttpGet]
        public async Task<ApiActionResult> GetGroupByCodeAsync(string code)
        {
            var response = await mediator.Send(new GetGroupByCodeDto { Code = code });

            return response;
        }

        [Route("findByName")]
        [HttpGet]
        public async Task<ApiActionResult> GetGroupByNameAsync(string name)
        {
            var response = await mediator.Send(new GetGroupByNameDto { Name = name });

            return response;
        }

        [Route("join")]
        [HttpPost]
        public async Task<ApiActionResult> AddUserToGroup(JoinGroupDto joinGroupModel)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || joinGroupModel.Email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"{joinGroupModel.Email}");
            }

            var response = await mediator.Send(joinGroupModel);

            return response.Result;
        }
    }
}
