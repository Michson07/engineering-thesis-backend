using Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Core.Database
{
    public class ValueObjectTypeConverter<T, TInner> : ValueConverter<T, TInner> where T : ValueObject<TInner>? where TInner : IComparable
    {
        public ValueObjectTypeConverter() : base(
            e => e,
            value => (T)Activator.CreateInstance(typeof(T), value)!)
        {

        }
    }
}
