using Core.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Users.Application.Commands;
using Users.Application.Queries;
using Users.Application.Queries.GetUserByPhoto;

namespace Users.Api
{
    [Route("api/user")]
    public class UsersController : BaseController
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ApiActionResult GetPhoto(string email)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"${email} photos");
            }

            var response = mediator.Send(new GetUserByPhotoDto { Email = email });

            return response.Result;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ApiActionResult> GetAllUsersAsync()
        {
            var response = await mediator.Send(new GetAllUsersDto());

            return response;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddUserAsync(AddUserDto user)
        {
            var response = await mediator.Send(user);

            return response.Result;
        }

        [HttpPut]
        public async Task<ApiActionResult> UpdateProfileAsync(UpdateUserDto user)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || user.Email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"${user.Email} profile");
            }

            var response = await mediator.Send(user);

            return response.Result;
        }
    }
}
