using FluentValidation;

namespace Core.Domain.ValueObjects
{
    public class UrlString : ValueObject<string>
    {
        private UrlString()
        {

        }

        public UrlString(string value) : base(value)
        {
            new UrlStringValidation().ValidateAndThrow(value);
        }
    }

    public class UrlStringValidation : AbstractValidator<string>
    {
        public UrlStringValidation()
        {
            RuleFor(x => x).Matches(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
        }
    }
}
