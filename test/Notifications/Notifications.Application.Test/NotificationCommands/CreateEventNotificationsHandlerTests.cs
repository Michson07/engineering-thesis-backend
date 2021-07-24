using Core.Domain.ValueObjects;
using Notifications.Application.NotificationCommands;
using Notifications.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notifications.Application.Test.NotificationCommands
{
    public class CreateEventNotificationsHandlerTests : NotificationServicesMock
    {
        private readonly Guid eventId = Guid.NewGuid();
        private readonly CreateEventNotificationsHandler handler;

        public CreateEventNotificationsHandlerTests()
        {
            repository.Add(NotificationAggregate.Create(eventId, new Email("user@mail.com")));

            handler = new CreateEventNotificationsHandler(mediator, repository);
        }

        [Fact]
        public async Task ShouldSendNotificationIfNotificationsWereNotSendAsync()
        {
            var request = new CreateEventNotificationsDto
            {
                Emails = new List<string>
                {
                    "user1@email.com",
                    "user2@o2.com"
                },
                EventId = "FC446827-6146-4768-9959-209E50FEF62B",
                Message = "Some important information",
                Title = "Information"
            };

            await handler.Handle(request, CancellationToken.None);
            await mediator.Received(2).Send(Arg.Any<SendEmailNotificationDto>());
        }

        [Fact]
        public async Task ShouldNotSendNotificationIfNotificationsWereSendAsync()
        {
            var request = new CreateEventNotificationsDto
            {
                Emails = new List<string>
                {
                    "user1@email.com",
                    "user2@o2.com"
                },
                EventId = eventId.ToString(),
                Message = "Some important information",
                Title = "Information"
            };

            await handler.Handle(request, CancellationToken.None);
            await mediator.Received(0).Send(Arg.Any<SendEmailNotificationDto>());
        }
    }
}
