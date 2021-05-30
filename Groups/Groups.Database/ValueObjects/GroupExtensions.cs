using Core.Database;
using Groups.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.ValueObjects
{
    public static class GroupExtensions
    {
        public static PropertyBuilder<GroupName> IsGroupName(this PropertyBuilder<GroupName> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<GroupName, string>());
        }

        public static PropertyBuilder<GroupAccessCode?> IsGroupCode(this PropertyBuilder<GroupAccessCode?> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<GroupAccessCode?, string>());
        }
    }
}
