using Core.Database;
using Groups.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groups.Database.AnnouncementAggregateDatabase
{
    public interface IAnnouncementAggregateRepository : IAggregateRepository
    {
        public Task Add(AnnouncementAggregate announcement);
        public void Update(AnnouncementAggregate announcement);
        public Task<AnnouncementAggregate>? GetById(string id);
        public Task<IEnumerable<AnnouncementAggregate>> GetAnnouncementsForUser(string email);
    }
}
