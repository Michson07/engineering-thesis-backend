using Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.ValueObjects
{
    public static class EmailExtensions
    {
        public static PropertyBuilder<Email> IsEmail(this PropertyBuilder<Email> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<Email, string>());
        }
    }
}
