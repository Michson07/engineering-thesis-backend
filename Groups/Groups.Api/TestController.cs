using Core.Api;
using Groups.Application;
using Groups.Application.TestCommands;
using Groups.Application.TestQueries;
using Groups.Application.TestResultCommands;
using Groups.Application.TestResultQueries;
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

        [Route("user-all")]
        [HttpGet]
        public async Task<ApiActionResult> GetUserTests(string email)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"read ${email} tests");
            }

            var response = await mediator.Send(new GetUserTestsDto { Email = email });

            return response;
        }

        [Route("solve")]
        [HttpPost]
        public async Task<ApiActionResult> AddTestResultAsync(AddTestResultDto testResult)
        {
            var response = await mediator.Send(testResult);

            return response.Result;
        }

        [Route("result")]
        [HttpGet]
        public async Task<ApiActionResult> GetTestResult(string email, string testId)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"get ${email} test result");
            }

            var response = await mediator.Send(new GetTestResultDto { Email = email, TestId = testId });

            return response;
        }

        [Route("students-results")]
        [HttpGet]
        public async Task<ApiActionResult> GetTestStudentsResults(string email, string testId)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"read ${email} students results");
            }

            var response = await mediator.Send(new GetTestStudentsResultsDto { Email = email, TestId = testId });

            return response;
        }

        [Route("update-checked-test")]
        [HttpPut]
        public async Task<ApiActionResult> UpdateTestResultStatus(UpdateTestResultStatusDto updateTestResultStatus)
        {
            var principalEmail = GetPrincipalEmail();
            if (principalEmail == null || updateTestResultStatus.Email != principalEmail)
            {
                return new NotAllowedResult<string, string>(principalEmail ?? "unknown user", $"${updateTestResultStatus.Email} tests checking");
            }

            var response = await mediator.Send(updateTestResultStatus);

            return response.Result;
        }

        [Route("send-test-email")]
        [HttpPost]
        public async Task SendEmailToUsersAboutTests()
        {
            await mediator.Send(new SendNotificationsAboutTomorrowTest());
        }
    }
}
