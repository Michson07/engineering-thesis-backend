using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestQueries
{
    public class GetUserTestsHandler : IRequestHandler<GetUserTestsDto, QueryResult<List<TestBasicView>>>
    {
        private readonly ITestAggregateRepository repository;

        public GetUserTestsHandler(ITestAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<QueryResult<List<TestBasicView>>> Handle(GetUserTestsDto request, CancellationToken cancellationToken)
        {
            var tests = repository.GetAllUserTests(request.Email);
            var testsView = MapToTestsView(tests).ToList();

            return Task.FromResult(new QueryResult<List<TestBasicView>> { BodyResponse = testsView });
        }

        private IEnumerable<TestBasicView> MapToTestsView(IEnumerable<TestAggregate> tests)
        {
            var testsView = new List<TestBasicView>();
            foreach(var test in tests)
            {
                testsView.Add(new TestBasicView
                {
                    Name = test.Name,
                    RequirePhoto = test.RequirePhoto,
                    PassedFrom = test.PassedFrom,
                    TimeDuration = test.TimeDuration,
                    Date = test.Date
                });
            }

            return testsView;
        }
    }
}
