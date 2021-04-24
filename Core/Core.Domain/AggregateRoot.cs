using System;

namespace Core.Domain
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; init; }

        public AggregateRoot()
        {
            Id = Guid.NewGuid();
        }
    }
}
