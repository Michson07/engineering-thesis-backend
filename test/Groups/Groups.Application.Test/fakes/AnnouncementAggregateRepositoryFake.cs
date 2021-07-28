using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Application.Test.fakes
{
    public class AnnouncementAggregateRepositoryFake : IAnnouncementAggregateRepository
    {
        private readonly List<AnnouncementAggregate> repository = new List<AnnouncementAggregate>();

        public Task Add(AnnouncementAggregate announcement)
        {
            repository.Add(announcement);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<AnnouncementAggregate>> GetAnnouncementsForUser(string email)
        {
            return Task.FromResult(repository
                .Where(announcement => announcement.Group.Participients
                    .Any(user => user.Email == email)));
        }

        public Task<AnnouncementAggregate> GetById(string id)
        {
            return Task.FromResult(repository.FirstOrDefault(announcement => announcement.Id.ToString() == id));
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public void Update(AnnouncementAggregate announcement)
        {
            // not needed
        }
    }
}
