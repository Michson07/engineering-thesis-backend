using System.Collections.Generic;

namespace Core.Domain.ValueObjects
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
