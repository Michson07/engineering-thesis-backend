using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; init; }

        public PhoneNumber(string number)
        {
            Number = number;

            new PhoneNumberValidation().ValidateAndThrow(this);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }

    public class PhoneNumberValidation : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidation()
        {
            RuleFor(x => x.Number).SetValidator(new RegularExpressionValidator(@"^((?:[0-9]\-?){6,14}[0-9])|((?:[0-9]\x20?){6,14}[0-9])$"));
        }
    }
}
