using FluentValidation;

namespace Core.Domain.ValueObjects
{
    public class Email : ValueObject<string>
    {
        private Email()
        {

        }

        public Email(string value) : base(value)
        {
            new EmailValidation().ValidateAndThrow(value);
        }
    }

    public class EmailValidation : AbstractValidator<string>
    {
        public EmailValidation()
        {
            RuleFor(x => x).EmailAddress();
        }
    }
}
