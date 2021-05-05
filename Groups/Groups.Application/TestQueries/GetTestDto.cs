using Core.Application;
using MediatR;

namespace Groups.Application.TestQueries
{
    public class GetTestDto : IRequest<QueryResult<TestView>>
    {
        public string TestId { get; set; } = string.Empty;
    }
}
