using Core.Database.Test;
using FluentAssertions;
using Groups.Database.ResourceAggregateDatabase;
using Groups.Domain.Aggregates;
using Groups.Domain.Test.Aggregates;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.ResourceAggregateDatabase
{
    public class ResourceAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly IResourceAggregateRepository resourceRepository;

        public ResourceAggregateRepositoryTests()
        {
            resourceRepository = new ResourceAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddResourceToDatabase()
        {
            var group = new GroupAggregateBuilder().Build();
            var resource = ResourceAggregate.Create("resource 1", group);

            await resourceRepository.Add(resource);
            await resourceRepository.SaveChanges();

            var resourceInDb = dbContext.ResourceAggregate.Single();
            resourceInDb.Should().BeEquivalentTo(resource);
        }

        [Fact]
        public async Task ShouldGetGroupResourcesFromDatabase()
        {
            var group = new GroupAggregateBuilder().Build();
            var resource1 = ResourceAggregate.Create("resource 1", group);
            var resource2 = ResourceAggregate.Create("resource 2", group);

            dbContext.AddRange(resource1, resource2);
            await resourceRepository.SaveChanges();

            var resourceInDb = await resourceRepository.GetGroupResources(group.Id.ToString());
            resourceInDb.Should().BeEquivalentTo(new ResourceAggregateWithoutFile[]
            {
                new()
                {
                    Id = resource1.Id,
                    Name = resource1.Name,
                    Group = resource1.Group,
                    AddedDate = resource1.AddedDate,
                    FileName = null,
                    FileType = null,
                    Url = null
                },
                new()
                {
                    Id = resource2.Id,
                    Name = resource2.Name,
                    Group = resource2.Group,
                    AddedDate = resource2.AddedDate,
                    FileName = null,
                    FileType = null,
                    Url = null
                }
            });
        }

        [Fact]
        public async Task ShouldGetResourceByIdFromDatabase()
        {
            var group = new GroupAggregateBuilder().Build();
            var resource1 = ResourceAggregate.Create("resource 1", group);

            dbContext.Add(resource1);
            await resourceRepository.SaveChanges();

            var resourceInDb = await resourceRepository.GetResourceById(resource1.Id.ToString());
            resourceInDb.Should().BeEquivalentTo(resource1);
        }
    }
}
