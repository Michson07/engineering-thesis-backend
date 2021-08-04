using Core.Database;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Database.ResourceAggregateDatabase
{
    public class ResourceAggregateRepository : AggregateRepository<GroupsDbContext>, IResourceAggregateRepository
    {
        public ResourceAggregateRepository(GroupsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Add(ResourceAggregate resourceAggregate)
        {
            await dbContext.AddAsync(resourceAggregate);
        }

        public async Task<IEnumerable<ResourceAggregateWithoutFile>> GetGroupResources(string groupId)
        {
            return await dbContext
                 .ResourceAggregate
                 .Include(r => r.Group)
                 .Where(r => r.Group.Id.ToString() == groupId)
                 .Select(resource => new ResourceAggregateWithoutFile
                 {
                     Id = resource.Id,
                     Name = resource.Name,
                     Group = resource.Group,
                     AddedDate = resource.AddedDate,
                     FileName = resource.File != null ? resource.File.Name : null,
                     FileType = resource.File != null ? resource.File.Type : null,
                     Url = resource.Url
                 })
                 .ToListAsync();
        }

        public async Task<ResourceAggregate> GetResourceById(string id)
        {
            return await dbContext
                .ResourceAggregate
                .FirstOrDefaultAsync(resource => resource.Id.ToString() == id);
        }
    }

    public class ResourceAggregateWithoutFile
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public GroupAggregate Group { get; set; } = null!;
        public DateTime AddedDate { get; set; }
        public UrlString? Url { get; set; }
    }
}
