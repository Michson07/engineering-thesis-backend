using Core.Domain;
using Core.Domain.ValueObjects;
using FluentValidation;
using Users.Domain.ValueObjects;

namespace Users.Domain.Aggregates
{
    public class UserAggregate : Aggregate
    {
        public Email Email { get; private set; } = null!;
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Photo? Photo { get; private set; }

        private UserAggregate()
        {
        }

        private UserAggregate(Email email, string name, string lastName, Photo? photo) : base()
        {
            Email = email;
            Name = name;
            LastName = lastName;
            Photo = photo;
        }

        public static UserAggregate Create(Email email, string name, string lastName, Photo? photo)
        {
            var user = new UserAggregate(email, name, lastName, photo);
            new UserAggregateValidation().ValidateAndThrow(user);

            return user;
        }
    }

    public class UserAggregateValidation : AbstractValidator<UserAggregate>
    {
        public UserAggregateValidation()
        {
            RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.LastName).NotEmpty();
        }
    }
}
