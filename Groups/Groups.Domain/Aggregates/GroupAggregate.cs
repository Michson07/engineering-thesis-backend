using Core.Domain;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.Aggregates
{
    public class GroupAggregate : AggregateRoot
    {
        public IEnumerable<Participient> Participients { get; private set; } = null!;
        public GroupName GroupName { get; private set; } = null!;

        private GroupAggregate()
        {
        }

        private GroupAggregate(IEnumerable<Participient> participients, GroupName groupName) : base()
        {
            Participients = participients;
            GroupName = groupName;
        }

        public static GroupAggregate Create(IEnumerable<Participient> participients, GroupName groupName)
        {
            var group = new GroupAggregate(participients, groupName);

            return group;
        }
    }
}
