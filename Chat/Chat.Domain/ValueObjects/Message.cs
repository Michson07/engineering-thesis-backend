using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Chat.Domain.ValueObjects
{
    public class Message : ValueObject
    {
        public Email UserEmail { get; init; }
        public string Text { get; init; }
        public DateTime Date { get; init; }

        private Message()
        {

        }

        public Message(Email userEmail, string text)
        {
            UserEmail = userEmail;
            Text = text;
            Date = DateTime.Now;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserEmail;
            yield return Text;
            yield return Date;
        }
    }
}
