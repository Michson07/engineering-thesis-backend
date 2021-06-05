using Core.Api;
using Groups.Application.ResourcesCommands;
using Groups.Application.ResourcesQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Groups.Api
{
    [Route("api/resources")]
    public class ResourcesController : BaseController
    {
        private readonly IMediator mediator;

        public ResourcesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddResourceAsync(AddResourceDto resource)
        {
            var response = await mediator.Send(resource);

            return response.Result;
        }

        [HttpGet]
        public async Task<ApiActionResult> GetGroupResources(string groupId)
        {
            var response = await mediator.Send(new GetResourcesDto { GroupId = groupId });

            return response;
        }

        [Route("value")]
        [HttpGet]
        public async Task<ApiActionResult> GetResourceValue(string resourceId)
        {
            var response = await mediator.Send(new GetResourceValueDto { Id = resourceId });

            return response;
        }
    }
}
