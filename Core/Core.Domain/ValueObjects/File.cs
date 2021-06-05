using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class File : ValueObject
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public byte[] Value { get; init; }

        private File()
        {
        }

        public File(string name, string type, byte[] value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
            yield return Value;
        }
    }
}
