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
            RuleFor(x => x.Number).SetValidator(new RegularExpressionValidator(@"^[2-9]\d{2}-\d{3}-\d{4}$"));
        }
    }
}
