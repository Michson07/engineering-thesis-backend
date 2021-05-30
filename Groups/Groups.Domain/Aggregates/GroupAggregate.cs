using Core.Domain;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.Aggregates
{
    public class GroupAggregate : AggregateRoot
    {
        public IEnumerable<Participient> Participients { get; private set; } = null!;
        public GroupName GroupName { get; private set; } = null!;
        public string Description { get; private set; } = string.Empty;
        public GroupAccessCode? Code { get; private set; } = null!;

        private GroupAggregate()
        {
        }

        private GroupAggregate(IEnumerable<Participient> participients, GroupName groupName, string description, bool open) : base()
        {
            Participients = participients;
            GroupName = groupName;
            Description = description;
            Code = open ? null : new GroupAccessCode();
        }

        public static GroupAggregate Create(IEnumerable<Participient> participients, GroupName groupName, string description, bool open = true)
        {
            var group = new GroupAggregate(participients, groupName, description, open);

            return group;
        }

        public GroupAggregate Update(IEnumerable<Participient> participients, GroupName groupName, string description, bool open = true)
        {
            Participients = participients;
            GroupName = groupName;
            Description = description;
            Code = open ? null : new GroupAccessCode();

            return this;
        }
    }
}
