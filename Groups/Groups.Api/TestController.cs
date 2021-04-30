using Core.Api;
using Groups.Application.TestCommands;
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
    }
}
