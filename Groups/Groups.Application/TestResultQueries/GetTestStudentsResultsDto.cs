using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.TestResultQueries
{
    public class GetTestStudentsResultsDto : IRequest<QueryResult<List<TestStudentsResultsView>>>
    {
        public string Email { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
    }
}
