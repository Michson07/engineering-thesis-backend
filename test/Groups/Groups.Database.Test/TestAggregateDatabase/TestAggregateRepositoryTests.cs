using Core.Database.Test;
using FluentAssertions;
using Groups.Database.TestAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.TestAggregateDatabase
{
    public class TestAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly ITestAggregateRepository testRepository;

        public TestAggregateRepositoryTests()
        {
            testRepository = new TestAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddTestToDatabase()
        {
            var test = new TestAggregateBuilder().Build();

            await testRepository.Add(test);
            await testRepository.SaveChanges();

            var testInDb = dbContext.TestAggregate.Single();
            testInDb.Should().BeEquivalentTo(test);
        }

        [Fact]
        public async Task ShouldGetGroupTestsFromDatabase()
        {
            var group = new GroupAggregateBuilder().Build();
            var test1 = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            var test2 = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            dbContext.AddRange(test1, test2);
            await testRepository.SaveChanges();

            var testsInDb = testRepository.GetGroupTests(group.GroupName);
            testsInDb.Should().BeEquivalentTo(test1, test2);
        }

        [Theory]
        [InlineData("owner@mail.com", true)]
        [InlineData("student@mail.com", false)]
        public async Task ShouldVerifyIfUserCanCheckTestResults(string email, bool expectedResult)
        {
            var participients = new List<Participient>
            {
                new(new("owner@mail.com"), GroupRoles.Owner)
            };

            if (email.Contains("student"))
            {
                participients.Add(new(new(email), GroupRoles.Student));
            }

            var group = new GroupAggregateBuilder()
                .WithParticipients(participients)
                .Build();

            var test = new TestAggregateBuilder()
                .WithGroup(group)
                .Build();

            dbContext.Add(test);
            await testRepository.SaveChanges();

            var isAllowed = await testRepository.UserIsAllowedToCheckResults(test.Id.ToString(), email);
            Assert.Equal(expectedResult, isAllowed);
        }

        [Fact]
        public async Task ShouldGetAllUserTests()
        {
            var group1 = new GroupAggregateBuilder()
                .WithParticipients(new Participient[] { new(new("user@mail.com"), GroupRoles.Owner) })
                .WithGroupName("group1")
                .Build();

            var group2 = new GroupAggregateBuilder()
                .WithParticipients(new Participient[] { new(new("user@mail.com"), GroupRoles.Owner) })
                .WithGroupName("group2")
                .Build();

            var test1 = new TestAggregateBuilder()
                .WithGroup(group1)
                .Build();

            var test2 = new TestAggregateBuilder()
                .WithGroup(group2)
                .Build();

            dbContext.AddRange(test1, test2);
            await testRepository.SaveChanges();

            var testsInDb = testRepository.GetAllUserTests("user@mail.com");
            testsInDb.Should().BeEquivalentTo(test1, test2);
        }

        [Fact]
        public async Task ShouldGetAllFutureTests()
        {
            var dateNow = DateTime.UtcNow;
            var futureTest1 = new TestAggregateBuilder()
                .WithDate(DateTime.MaxValue.AddDays(-10))
                .Build();

            var futureTest2 = new TestAggregateBuilder()
                .WithDate(DateTime.MaxValue)
                .Build();

            var pastTest = new TestAggregateBuilder()
                .WithDate(dateNow.AddDays(-2))
                .Build();

            dbContext.AddRange(futureTest1, futureTest2, pastTest);
            await testRepository.SaveChanges();

            var testsInDb = await testRepository.GetFutureTests(dateNow);
            testsInDb.Should().BeEquivalentTo(futureTest1, futureTest2);
        }
    }
}
