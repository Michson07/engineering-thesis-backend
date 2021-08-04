using Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.ValueObjects
{
    public static class UrlStringExtensions
    {
        public static PropertyBuilder<UrlString?> IsUrlString(this PropertyBuilder<UrlString?> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<UrlString?, string>());
        }
    }
}
