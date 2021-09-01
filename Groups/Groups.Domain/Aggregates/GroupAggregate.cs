using Core.Domain;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Domain.Aggregates
{
    public class GroupAggregate : AggregateRoot
    {
        public IEnumerable<Participient> Participients { get; private set; } = null!;
        public GroupName GroupName { get; private set; } = null!;
        public string Description { get; private set; } = string.Empty;
        public GroupAccessCode? Code { get; private set; }

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
            new GroupAggregateValidator().ValidateAndThrow(group);

            return group;
        }

        public GroupAggregate Update(IEnumerable<Participient> participients, GroupName groupName, string description, bool open = true)
        {
            var group = new GroupAggregate(participients, groupName, description, open);
            new GroupAggregateValidator().ValidateAndThrow(group);

            Participients = participients;
            GroupName = groupName;
            Description = description;
            Code = open ? null : new GroupAccessCode();

            return this;
        }
    }

    public class GroupAggregateValidator : AbstractValidator<GroupAggregate>
    {
        public GroupAggregateValidator()
        {
            RuleFor(x => x.Participients).NotEmpty();
            RuleFor(x => x.Participients.Where(participient => participient.Role == GroupRoles.Owner)).NotEmpty();
        }
    }
}
