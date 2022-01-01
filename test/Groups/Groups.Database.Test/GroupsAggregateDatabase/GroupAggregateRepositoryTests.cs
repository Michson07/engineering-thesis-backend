using Core.Database.Test;
using FluentAssertions;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Domain.Test.Aggregates;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.GroupsAggregateDatabase
{
    public class GroupAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly IGroupAggregateRepository groupAggregateRepository;

        public GroupAggregateRepositoryTests()
        {
            groupAggregateRepository = new GroupAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddGroupToDatabase()
        {
            var group = new GroupAggregateBuilder().Build();

            groupAggregateRepository.Add(group);
            await groupAggregateRepository.SaveChanges();

            var groupInDb = dbContext.GroupAggregate.Single();
            groupInDb.Should().BeEquivalentTo(group);
        }

        [Fact]
        public async Task ShouldGetGroupByName()
        {
            var groupName = "group1";
            var group = new GroupAggregateBuilder()
                .WithGroupName(groupName)
                .Build();

            dbContext.GroupAggregate.Add(group);
            await groupAggregateRepository.SaveChanges();

            var groupInDb = await groupAggregateRepository.GetByName(groupName);
            groupInDb.Should().BeEquivalentTo(group);
        }

        [Fact]
        public async Task ShouldGetGroupByCode()
        {
            var group = new GroupAggregateBuilder()
                .WithIsOpen(false)
                .Build();

            dbContext.GroupAggregate.Add(group);
            await groupAggregateRepository.SaveChanges();

            var groupInDb = await groupAggregateRepository.GetByCode(group.Code);
            groupInDb.Should().BeEquivalentTo(group);
        }

        [Fact]
        public async Task ShouldGetGroupById()
        {
            var group = new GroupAggregateBuilder()
                .Build();

            dbContext.GroupAggregate.Add(group);
            await groupAggregateRepository.SaveChanges();

            var groupInDb = groupAggregateRepository.GetById(group.Id.ToString());
            groupInDb.Should().BeEquivalentTo(group);
        }

        [Fact]
        public async Task ShouldGetUserGroups()
        {
            var group1 = new GroupAggregateBuilder()
                .WithGroupName("group1")
                .WithDescription("some group 1")
                .WithIsOpen(true)
                .Build();

            var group2 = new GroupAggregateBuilder()
                .WithGroupName("group2")
                .WithDescription("some group 2")
                .WithIsOpen(false)
                .Build();

            dbContext.AddRange(group1, group2);
            await groupAggregateRepository.SaveChanges();

            var groupInDb = await groupAggregateRepository.GetUserGroups(group1.Participients.Single().Email);
            groupInDb.Should().BeEquivalentTo(group1, group2);
        }
    }
}
