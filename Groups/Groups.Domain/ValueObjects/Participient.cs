using Core.Domain.ValueObjects;
using FluentValidation;
using System.Collections.Generic;

namespace Groups.Domain.ValueObjects
{
    public class Participient : ValueObject
    {
        public Email Email { get; private set; } = null!;
        public string Role { get; private set; } = null!;

        private Participient()
        {
        }

        private Participient(Email email, string role)
        {
            Email = email;
            Role = role;
        }

        public static Participient Create(Email email, string role) //todo check if I should do this with Create or constructor
        {
            var participient = new Participient(email, role);
            new ParticipientValidation().ValidateAndThrow(participient);

            return participient;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
            yield return Role;
        }
    }

    public class ParticipientValidation : AbstractValidator<Participient>
    {
        public ParticipientValidation()
        {
            RuleFor(participient => participient.Role).Must(p => p == "Owner" || p == "Student");
        }
    }
}
