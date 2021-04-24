using Core.Domain.ValueObjects;
using FluentValidation;
using System.Collections.Generic;

namespace Groups.Domain.ValueObjects
{
    public class GroupName : ValueObject
    {
        public string Name { get; init; }

        private GroupName(string name)
        {
            Name = name;
        }

        public static GroupName Create(string name)
        {
            var groupName = new GroupName(name);
            new GroupNameValidation().ValidateAndThrow(groupName);

            return groupName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }

    public class GroupNameValidation : AbstractValidator<GroupName>
    {
        public GroupNameValidation()
        {
            RuleFor(name => name.Name).NotEmpty().MinimumLength(3).MaximumLength(15);
        }
    }
}
