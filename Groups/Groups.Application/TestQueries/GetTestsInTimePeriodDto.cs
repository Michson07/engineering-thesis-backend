using Core.Application;
using MediatR;
using System;
using System.Collections.Generic;

namespace Groups.Application.TestQueries
{
    public class GetTestsInTimePeriodDto : IRequest<QueryResult<List<TestInTimePeriodView>>>
    {
        public DateTime Time { get; set; }
    }
}
