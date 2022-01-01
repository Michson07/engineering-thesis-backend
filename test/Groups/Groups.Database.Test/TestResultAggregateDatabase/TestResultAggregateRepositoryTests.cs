using Core.Database.Test;
using FluentAssertions;
using Groups.Database.TestResultAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.TestResultAggregateDatabase
{
    public class TestResultAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly ITestResultAggregateRepository testResultRepository;

        public TestResultAggregateRepositoryTests()
        {
            testResultRepository = new TestResultAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddTestResultToDatabase()
        {
            var testResult = new TestResultAggregateBuilder().Build();

            testResultRepository.Add(testResult);
            await testResultRepository.SaveChanges();

            var testInDb = dbContext.TestResultAggregate.Single();
            testInDb.Should().BeEquivalentTo(testResult);
        }

        [Fact]
        public async Task ShouldGetStudentTestFromDatabase()
        {
            var studentEmail = "someStudent@mail.com";

            var test = new TestAggregateBuilder().Build();

            var testResult = new TestResultAggregateBuilder()
                .WithStudent(new(new(studentEmail), GroupRoles.Student))
                .WithTest(test)
                .Build();

            dbContext.TestResultAggregate.Add(testResult);
            await testResultRepository.SaveChanges();

            var testResultInDb = await testResultRepository.GetTestResult(studentEmail, test.Id.ToString());
            testResultInDb.Should().BeEquivalentTo(testResult);
        }

        [Fact]
        public async Task ShouldGetTestStudents()
        {
            var testStudent1 = new Participient(new("someStudent1@mail.com"), GroupRoles.Owner);
            var testStudent2 = new Participient(new("someStudent2@mail.com"), GroupRoles.Student);
            var testStudent3 = new Participient(new("someStudent3@mail.com"), GroupRoles.Owner);

            var group1 = new GroupAggregateBuilder()
                .WithParticipients(new[] { testStudent1, testStudent2 })
                .Build();

            var group2 = new GroupAggregateBuilder()
                .WithParticipients(new[] { testStudent3 })
                .Build();

            var test1 = new TestAggregateBuilder()
                .WithGroup(group1)
                .Build();

            var test2 = new TestAggregateBuilder()
                .WithGroup(group2)
                .Build();

            var testResult1 = new TestResultAggregateBuilder()
                .WithStudent(testStudent1)
                .WithTest(test1)
                .Build();

            var testResult2 = new TestResultAggregateBuilder()
                .WithStudent(testStudent2)
                .WithTest(test1)
                .Build();

            var testResult3 = new TestResultAggregateBuilder()
                .WithStudent(testStudent3)
                .WithTest(test2)
                .Build();

            dbContext.TestResultAggregate.AddRange(testResult1, testResult2);
            await testResultRepository.SaveChanges();

            var testResultInDb = await testResultRepository.GetTestStudents(test1.Id.ToString());
            testResultInDb.Should().BeEquivalentTo(testStudent1.Email, testStudent2.Email);
        }
    }
}
