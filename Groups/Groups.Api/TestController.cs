using Core.Api;
using Groups.Application.TestCommands;
using Groups.Application.TestQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Groups.Api
{
    [Route("api/test")]
    public class TestController : BaseController
    {
        private readonly IMediator mediator;

        public TestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiActionResult> AddTestAsync(AddTestDto test)
        {
            var response = await mediator.Send(test);

            return response.Result;
        }

        [HttpGet]
        public async Task<ApiActionResult> GetTestAsync(string testId)
        {
            var response = await mediator.Send(new GetTestDto { TestId = testId });

            return response;
        }
    }
}
