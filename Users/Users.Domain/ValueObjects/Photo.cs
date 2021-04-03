using Core.Domain.ValueObjects;
using System.Collections.Generic;

namespace Users.Domain.ValueObjects
{
    public class Photo : ValueObject
    {
        public byte[] Image { get; init; }

        private Photo()
        {
        }

        public Photo(byte[] photo)
        {
            Image = photo;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Image;
        }
    }
}
