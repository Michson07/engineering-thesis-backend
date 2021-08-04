using Core.Application;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain.Aggregates;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Groups.Application.TestResultQueries
{
    public class GetTestResultHandler : IRequestHandler<GetTestResultDto, QueryResult<TestResultView>>
    {
        private readonly ITestResultAggregateRepository repository;
        private readonly ITestAggregateRepository testRepository;

        public GetTestResultHandler(ITestResultAggregateRepository repository, ITestAggregateRepository testRepository)
        {
            this.repository = repository;
            this.testRepository = testRepository;
        }

        public async Task<QueryResult<TestResultView>> Handle(GetTestResultDto request, CancellationToken cancellationToken)
        {
            var userResult = await repository.GetTestResult(request.Email, request.TestId);
            var test = await testRepository.GetTestById(request.TestId);

            if (userResult == null)
            {
                if(test == null || test.Date.AddMinutes(test.TimeDuration) > DateTime.Now)
                {
                    return new QueryResult<TestResultView> { BodyResponse = null };
                }

                userResult = TestResultAggregate.CreateForAbsent(test, test.Group.Participients.First(p => p.Email == request.Email));
                repository.Add(userResult);
            }

            var response = new TestResultMapper().MapToTestResultView(userResult);
            response.TestName = test!.Name;
            response.PassedFrom = test.PassedFrom;

            await repository.SaveChanges();

            return new QueryResult<TestResultView> { BodyResponse = response };
        }
    }
}
