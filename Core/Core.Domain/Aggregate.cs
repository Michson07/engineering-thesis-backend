using System;

namespace Core.Domain
{
    public abstract class Aggregate
    {
        public Guid Id { get; init; }

        public Aggregate()
        {
            Id = Guid.NewGuid();
        }
    }
}
