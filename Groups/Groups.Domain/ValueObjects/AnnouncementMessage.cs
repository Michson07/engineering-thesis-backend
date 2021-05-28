using Core.Domain.ValueObjects;

namespace Groups.Domain.ValueObjects
{
    public class AnnouncementMessage : ValueObject<string>
    {
        public AnnouncementMessage(string value) : base(value)
        {
        }
    }
}
