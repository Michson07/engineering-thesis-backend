using Core.Database;
using Groups.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database.ValueObjects
{
    public static class AnnouncementExtensions
    {
        public static PropertyBuilder<AnnouncementTitle> IsAnnouncementTitle(this PropertyBuilder<AnnouncementTitle> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<AnnouncementTitle, string>());
        }

        public static PropertyBuilder<AnnouncementMessage> IsAnnouncementMessage(this PropertyBuilder<AnnouncementMessage> builder)
        {
            return builder
                .HasConversion(new ValueObjectTypeConverter<AnnouncementMessage, string>());
        }
    }
}
