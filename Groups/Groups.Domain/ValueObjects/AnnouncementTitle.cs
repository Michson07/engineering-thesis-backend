using Core.Domain.ValueObjects;
using FluentValidation;

namespace Groups.Domain.ValueObjects
{
    public class AnnouncementTitle : ValueObject<string>
    {
        public AnnouncementTitle(string value) : base(value)
        {
            new AnnouncementTitleValidation().ValidateAndThrow(value);
        }
    }

    public class AnnouncementTitleValidation : AbstractValidator<string>
    {
        public AnnouncementTitleValidation()
        {
            RuleFor(x => x).MaximumLength(100).WithMessage("Tytuł jest zbyt długi");
        }
    }
}
