using Core.Domain.Test;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.Test.Aggregates
{
    public class GroupAggregateBuilder : Builder<GroupAggregate>
    {
        private IEnumerable<Participient> participients = new List<Participient>()
        {
            new Participient(new Email("creator@mail.com"), GroupRoles.Owner)
        };

        private GroupName groupName = new GroupName("Group1");
        private string description = "Some description of Group1";
        private bool open = true;

        protected override GroupAggregate BuildInstance()
        {
            return GroupAggregate.Create(participients, groupName, description, open);
        }

        public GroupAggregateBuilder WithParticipients(IEnumerable<Participient> participients)
        {
            this.participients = participients;

            return this;
        }

        public GroupAggregateBuilder WithGroupName(string groupName)
        {
            this.groupName = new GroupName(groupName);

            return this;
        }

        public GroupAggregateBuilder WithDescription(string description)
        {
            this.description = description;

            return this;
        }

        public GroupAggregateBuilder WithIsOpen(bool open)
        {
            this.open = open;

            return this;
        }
    }
}
