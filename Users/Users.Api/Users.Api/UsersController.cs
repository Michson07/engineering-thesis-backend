using Core.Api;
using Core.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands;
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
        public ActionResult<GetUserByPhotoView> GetPhoto(string email)
        {
            var response = mediator.Send(new GetUserByPhotoDto { Email = email });

            return response.Result.Body;
        }

        [HttpPost]
        public ActionResult<ApiActionResult> AddUser(AddUserDto user)
        {
            var response = mediator.Send(user);

            return response.Result.Body;
        }

        //[HttpPut]
        //public ActionResult<ApiActionResult> UpdateProfile()
        //{
        //    var response = mediator.Send(new UpdateProfile());

        //    return response.Result.Body;
        //}
    }
}
