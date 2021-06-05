using Core.Application;
using Groups.Database.TestAggregateDatabase;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestQueries
{
    public class GetTestsInTimePeriodHandler : IRequestHandler<GetTestsInTimePeriodDto, QueryResult<List<TestInTimePeriodView>>>
    {
        private readonly ITestAggregateRepository testRepository;

        public GetTestsInTimePeriodHandler(ITestAggregateRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<QueryResult<List<TestInTimePeriodView>>> Handle(GetTestsInTimePeriodDto request, CancellationToken cancellationToken)
        {
            var futureTests = await testRepository.GetFutureTests(request.Time);
            var tomorrowTests = futureTests.Where(ft => ft.Date.Day == request.Time.AddDays(1).Day);

            return new QueryResult<List<TestInTimePeriodView>>
            {
                BodyResponse = tomorrowTests.Select(tt => new TestInTimePeriodView
                {
                    TestId = tt.Id.ToString(),
                    TestName = tt.Name,
                    TestDate = tt.Date,
                    TestTimeDuration = tt.TimeDuration,
                    TestRequirePhoto = tt.RequirePhoto,
                    Emails = tt.Group.Participients.Select(p => p.Email.ToString())
                }).ToList()
            };
        }
    }
}
