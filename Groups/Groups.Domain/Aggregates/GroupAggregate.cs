using Core.Domain;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.Aggregates
{
    public class GroupAggregate : AggregateRoot
    {
        public IEnumerable<Participient> Participients { get; private set; } = null!;
        public GroupName GroupName { get; private set; } = null!;
        public string Description { get; set; } = string.Empty;

        private GroupAggregate()
        {
        }

        private GroupAggregate(IEnumerable<Participient> participients, GroupName groupName, string description) : base()
        {
            Participients = participients;
            GroupName = groupName;
            Description = description;
        }

        public static GroupAggregate Create(IEnumerable<Participient> participients, GroupName groupName, string description)
        {
            var group = new GroupAggregate(participients, groupName, description);

            return group;
        }
    }
}
