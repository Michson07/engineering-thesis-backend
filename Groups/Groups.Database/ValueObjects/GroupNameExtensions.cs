using Core.Database;
using Groups.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.ValueObjects
{
    public static class GroupNameExtensions
    {
        public static PropertyBuilder<GroupName> IsGroupName(this PropertyBuilder<GroupName> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<GroupName, string>());
        }
    }
}
