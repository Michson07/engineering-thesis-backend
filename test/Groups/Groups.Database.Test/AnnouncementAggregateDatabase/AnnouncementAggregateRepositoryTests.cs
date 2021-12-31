using Core.Database.Test;
using Core.Domain.ValueObjects;
using FluentAssertions;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Domain;
using Groups.Domain.Test.Aggregates;
using Groups.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Groups.Database.Test.AnnouncementAggregateDatabase
{
    public class AnnouncementAggregateRepositoryTests : DatabaseTestConfiguration<GroupsDbContext>
    {
        private readonly IAnnouncementAggregateRepository announcementRepository;

        public AnnouncementAggregateRepositoryTests()
        {
            announcementRepository = new AnnouncementAggregateRepository(dbContext);
        }

        [Fact]
        public async Task ShouldAddAnnouncementToDatabase()
        {
            var announcement = new AnnouncementAggregateBuilder().Build();

            await announcementRepository.Add(announcement);
            await announcementRepository.SaveChanges();

            var announcementInDb = dbContext.AnnouncementAggregate.Single();
            announcementInDb.Should().BeEquivalentTo(announcement);
        }

        [Fact]
        public async Task ShouldGetAnnouncementById()
        {
            var announcement = new AnnouncementAggregateBuilder().Build();

            dbContext.AnnouncementAggregate.Add(announcement);
            await announcementRepository.SaveChanges();
            
            var announcementInDb = await announcementRepository.GetById(announcement.Id.ToString());
            announcementInDb.Should().BeEquivalentTo(announcement);
        }

        [Fact]
        public async Task ShouldGetAnnouncementForUser()
        {
            var userEmail = new Email("someuser@gmail.com");
            var groupOwner = new Participient(new("owner@mail.com"), GroupRoles.Owner);

            var group = new GroupAggregateBuilder()
                .WithParticipients(new Participient[]
                {
                    new(userEmail, GroupRoles.Student),
                    groupOwner
                })
                .Build();

            var announcement = new AnnouncementAggregateBuilder()
                .WithGroupAggregate(group)
                .WithCreator(groupOwner)
                .Build();

            dbContext.AnnouncementAggregate.Add(announcement);
            await announcementRepository.SaveChanges();

            var announcementInDb = await announcementRepository.GetAnnouncementsForUser(userEmail);
            announcementInDb.Should().BeEquivalentTo(announcement);
        }
    }
}
