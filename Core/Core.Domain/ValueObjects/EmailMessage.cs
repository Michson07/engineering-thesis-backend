using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class EmailMessage : ValueObject
    {
        public string Title { get; init; }
        public string Message { get; init; }

        public EmailMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Message;
        }
    }
}
