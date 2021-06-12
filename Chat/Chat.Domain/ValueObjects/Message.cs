using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Chat.Domain.ValueObjects
{
    public class Message : ValueObject
    {
        public Guid UserId { get; init; }
        public string Text { get; init; }
        public DateTime Date { get; init; }

        private Message()
        {

        }

        public Message(string userId, string text)
        {
            UserId = new Guid(userId);
            Text = text;
            Date = DateTime.Now;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Text;
            yield return Date;
        }
    }
}
