using Core.Api;
using Groups.Application.AnnouncementCommands;
using Groups.Application.AnnouncementQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ApiActionResult> GetAnnouncementForUserAsync(GetUserAnnouncementsDto user)
        {
            var response = await mediator.Send(user);

            return response;
        }
    }
}
