using Core.Domain.ValueObjects;
using System.Collections.Generic;

namespace Groups.Domain.ValueObjects
{
    public class Answer : ValueObject
    {
        public string Value { get; init; } = null!;
        public bool Correct { get; set; } = false;

        private Answer()
        {
        }

        public Answer(string value, bool correct)
        {
            Value = value;
            Correct = correct;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Correct;
        }
    }
}
