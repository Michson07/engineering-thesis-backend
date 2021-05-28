using Core.Domain;
using FluentValidation;
using Groups.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groups.Domain.Aggregates
{
    public class AnnouncementAggregate : AggregateRoot
    {
        public AnnouncementTitle Title { get; private set; } = null!;
        public AnnouncementMessage Message { get; private set; } = null!;
        public GroupAggregate Group { get; private set; } = null!;
        public Participient Creator { get; private set; } = null!;
        public DateTime Date { get; set; }

        private AnnouncementAggregate()
        {

        }

        public AnnouncementAggregate(AnnouncementTitle title, AnnouncementMessage message, GroupAggregate group, Participient creator) : base()
        {
            Title = title;
            Message = message;
            Creator = creator;
            Group = group;
            Date = DateTime.Now;
        }

        public static AnnouncementAggregate Create(AnnouncementTitle title, AnnouncementMessage message, GroupAggregate group, Participient creator)
        {
            if (!group.Participients.Select(p => p.Email).Contains(creator.Email))
            {
                throw new Exception("Twórca nie należy do podanej grupy!");
            }

            var announcement = new AnnouncementAggregate(title, message, group, creator);

            new AnnouncementAggregateValidation().ValidateAndThrow(announcement);

            return announcement;
        }

        public AnnouncementAggregate Update(AnnouncementTitle title, AnnouncementMessage message)
        {
            Title = title;
            Message = message;

            return this;
        }
    }

    public class AnnouncementAggregateValidation : AbstractValidator<AnnouncementAggregate>
    {
        public AnnouncementAggregateValidation()
        {
            RuleFor(announcement => announcement.Creator.Role).Must(p => p == GroupRoles.Owner);
        }
    }
}
