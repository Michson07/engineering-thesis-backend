using Core.Domain;
using Core.Domain.ValueObjects;
using System;

namespace Notifications.Domain
{
    public class NotificationAggregate : AggregateRoot
    {
        public Guid EventId { get; private set; }
        public Email UserEmail { get; private set; }

        private NotificationAggregate()
        {
        }

        private NotificationAggregate(Guid eventId, Email userEmail)
        {
            EventId = eventId;
            UserEmail = userEmail;
        }

        public static NotificationAggregate Create(Guid eventId, Email userEmail)
        {
            return new NotificationAggregate(eventId, userEmail);
        }
    }
}
