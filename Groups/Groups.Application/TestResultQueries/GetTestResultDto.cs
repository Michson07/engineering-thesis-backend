using Core.Application;
using Groups.Application.TestResultQueries;
using MediatR;

namespace Groups.Application
{
    public class GetTestResultDto : IRequest<QueryResult<TestResultView>>
    {
        public string Email { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
    }
}
