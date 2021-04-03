using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string EmailAddress { get; init; }

        private Email()
        {

        }

        private Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public static Email Create(string emailAddress)
        {
            var email = new Email(emailAddress);
            new EmailValidation().ValidateAndThrow(email);

            return email;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }

    public class EmailValidation : AbstractValidator<Email>
    {
        public EmailValidation()
        {
            RuleFor(x => x.EmailAddress).EmailAddress();
        }
    }
}
