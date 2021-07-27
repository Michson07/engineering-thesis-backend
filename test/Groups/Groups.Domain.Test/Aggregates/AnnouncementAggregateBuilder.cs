using Core.Domain.Test;
using Core.Domain.ValueObjects;
using Groups.Domain.Aggregates;
using Groups.Domain.ValueObjects;

namespace Groups.Domain.Test.Aggregates
{
    public class AnnouncementAggregateBuilder : Builder<AnnouncementAggregate>
    {
        private AnnouncementTitle title = new AnnouncementTitle("Title");
        private AnnouncementMessage message = new AnnouncementMessage("Message");
        private GroupAggregate group = new GroupAggregateBuilder().Build();
        private Participient creator = new Participient(new Email("creator@mail.com"), GroupRoles.Owner);

        protected override AnnouncementAggregate BuildInstance()
        {
            return AnnouncementAggregate.Create(title, message, group, creator);
        }

        public AnnouncementAggregateBuilder WithTitle(string title)
        {
            this.title = new AnnouncementTitle(title);

            return this;
        }

        public AnnouncementAggregateBuilder WithMessage(string message)
        {
            this.message = new AnnouncementMessage(message);

            return this;
        }

        public AnnouncementAggregateBuilder WithGroupAggregate(GroupAggregate group)
        {
            this.group = group;

            return this;
        }

        public AnnouncementAggregateBuilder WithCreator(Participient creator)
        {
            this.creator = creator;

            return this;
        }
    }
}
