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

        public Participient(Email email, string role)
        {
            Email = email;
            Role = role;
            new ParticipientValidation().ValidateAndThrow(this);
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
            RuleFor(participient => participient.Role).Must(p => p == GroupRoles.Owner || p == GroupRoles.Student);
        }
    }
}
