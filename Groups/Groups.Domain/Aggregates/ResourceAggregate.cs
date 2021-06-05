using Core.Domain;
using Core.Domain.ValueObjects;
using System;

namespace Groups.Domain.Aggregates
{
    public class ResourceAggregate : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public File File { get; private set; } = null!;
        public GroupAggregate Group { get; set; } = null!;
        public DateTime AddedDate { get; set; }

        private ResourceAggregate()
        {
        }

        private ResourceAggregate(string name, File file, GroupAggregate group)
        {
            Name = name;
            File = file;
            Group = group;
            AddedDate = DateTime.Now;
        }

        public static ResourceAggregate Create(string name, File file, GroupAggregate group)
        {
            return new ResourceAggregate(name, file, group);
        }
    }
}
