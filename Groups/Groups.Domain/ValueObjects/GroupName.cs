using Core.Domain.ValueObjects;
using FluentValidation;

namespace Groups.Domain.ValueObjects
{
    public class GroupName : ValueObject<string>
    {
        private GroupName()
        {
        }

        public GroupName(string value) : base(value)
        {
            new GroupNameValidation().ValidateAndThrow(value);
        }
    }

    public class GroupNameValidation : AbstractValidator<string>
    {
        public GroupNameValidation()
        {
            RuleFor(name => name).NotEmpty().MinimumLength(3).MaximumLength(40);
        }
    }
}
