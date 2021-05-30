using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Groups.Application.TestQueries
{
    public class GetUserTestsDto : IRequest<QueryResult<List<TestBasicView>>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
