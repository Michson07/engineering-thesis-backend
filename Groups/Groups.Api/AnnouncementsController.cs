using Core.Api;
using Groups.Application.AnnouncementCommands;
using Groups.Application.AnnouncementQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Groups.Api
{
    [Route("api/announcements")]
    public class AnnouncementsController : BaseController
    {
        private readonly IMediator mediator;

        public AnnouncementsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddAnnouncementAsync(AddAnnouncementDto announcement)
        {
            var response = await mediator.Send(announcement);

            return response.Result;
        }

        [HttpPut]
        public async Task<ApiActionResult> UpdateAnnouncementAsync(UpdateAnnouncementDto announcement)
        {
            var response = await mediator.Send(announcement);

            return response.Result;
        }

        [HttpGet]
        public async Task<ApiActionResult> GetAnnouncementForUserAsync(string email)
        {
            var response = await mediator.Send(new GetUserAnnouncementsDto { Email = email });

            return response;
        }
    }
}
