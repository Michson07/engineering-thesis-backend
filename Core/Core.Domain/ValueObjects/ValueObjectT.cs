using System;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public abstract class ValueObject<T> : ValueObject, IComparable where T : IComparable
    {
        protected readonly T value;

        protected ValueObject()
        {
        }

        protected ValueObject(T value)
        {
            this.value = value;
        }

        public static implicit operator T(ValueObject<T> t) => t == null ? default! : t.value;

        public override string ToString()
        {
            return value.ToString();
        }

        public int CompareTo(object? other)
        {
            if (other == null)
            {
                return 1;
            }

            var otherValueObject = other as ValueObject<T>;
            if (otherValueObject == null)
            {
                throw new ArgumentException("Object is not ValueObject<T>");
            }

            return value.CompareTo(otherValueObject.value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}
