using Core.Application;
using Core.Domain.ValueObjects;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Queries;

namespace Groups.Application.TestResultQueries
{
    public class GetTestStudentsResultsHandler : IRequestHandler<GetTestStudentsResultsDto, QueryResult<List<TestStudentsResultsView>>>
    {
        private readonly ITestResultAggregateRepository testResultRepository;
        private readonly ITestAggregateRepository testRepository;
        private readonly IMediator mediator;

        public GetTestStudentsResultsHandler(ITestResultAggregateRepository testResultRepository,
            ITestAggregateRepository testRepository, IMediator mediator)
        {
            this.testResultRepository = testResultRepository;
            this.testRepository = testRepository;
            this.mediator = mediator;
        }

        public async Task<QueryResult<List<TestStudentsResultsView>>> Handle(GetTestStudentsResultsDto request, CancellationToken cancellationToken)
        {
            var isOwner = await testRepository.UserIsAllowedToCheckResults(request.TestId, request.Email);
            if(!isOwner)
            {
                throw new Exception("User is not an owner of a group!");
            }

            var testStudents = await testResultRepository.GetTestStudents(request.TestId);
            var view = await MapToViewAsync(testStudents, request.TestId);
            return new QueryResult<List<TestStudentsResultsView>> { BodyResponse = view };
        }

        public async Task<List<TestStudentsResultsView>> MapToViewAsync(IEnumerable<Email> emails, string testId)
        {
            var studentsResults = new List<TestStudentsResultsView>();
            var testResultMapper = new TestResultMapper();

            foreach(var email in emails)
            {
                var userView = mediator.Send(new GetUserByEmailDto { Email = email }).Result;
                var result = await testResultRepository.GetTestResult(email, testId);

                if (userView.BodyResponse != null && result != null)
                {
                    studentsResults.Add(new TestStudentsResultsView
                    {
                        Name = userView.BodyResponse.Name,
                        LastName = userView.BodyResponse.LastName,
                        Result = testResultMapper.MapToTestResultView(result)
                    });
                }
            }

            return studentsResults;
        }
    }
}
