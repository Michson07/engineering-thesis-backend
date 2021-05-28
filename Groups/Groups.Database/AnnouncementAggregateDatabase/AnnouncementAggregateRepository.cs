using Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groups.Domain.Aggregates;


namespace Groups.Database.AnnouncementAggregateDatabase
{
    public class AnnouncementAggregateRepository : AggregateRepository<GroupsDbContext>, IAnnouncementAggregateRepository
    {
        public AnnouncementAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(AnnouncementAggregate announcement)
        {
            await dbContext.AddAsync(announcement);
        }

        public async Task<IEnumerable<AnnouncementAggregate>> GetAnnouncementsForUser(string email)
        {
            var userGroups = await dbContext
                .GroupAggregate
                .Where(g => g.Participients.Any(p => p.Email == email))
                .Select(g => g.Id).ToListAsync();

            return await dbContext.AnnouncementAggregate.Where(a => userGroups.Any(ug => ug == a.Group.Id)).ToListAsync();
        }

        public async Task<AnnouncementAggregate>? GetById(string id)
        {
            return await dbContext.AnnouncementAggregate.FirstOrDefaultAsync(a => a.Id.ToString() == id);
        }

        public void Update(AnnouncementAggregate announcement)
        {
            dbContext.Update(announcement);
        }
    }
}
