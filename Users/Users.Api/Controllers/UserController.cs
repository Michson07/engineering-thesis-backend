using Core.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Queries.GetUserByPhoto;

namespace Users.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult<GetUserByPhotoView> GetPhoto(string email)
        {
            var response = mediator.Send(new GetUserByPhotoDto { Email = email });

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
