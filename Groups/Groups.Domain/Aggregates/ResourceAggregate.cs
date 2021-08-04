using Core.Domain;
using Core.Domain.ValueObjects;
using System;

namespace Groups.Domain.Aggregates
{
    public class ResourceAggregate : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public File? File { get; private set; }
        public UrlString? Url { get; private set; }
        public GroupAggregate Group { get; set; } = null!;
        public DateTime AddedDate { get; set; }

        private ResourceAggregate()
        {
        }

        private ResourceAggregate(string name, File? file, UrlString? url, GroupAggregate group)
        {
            Name = name;
            File = file;
            Url = url;
            Group = group;
            AddedDate = DateTime.Now;
        }

        public static ResourceAggregate Create(string name, GroupAggregate group, File? file = null, UrlString? url = null)
        {
            return new ResourceAggregate(name, file, url, group);
        }
    }
}
