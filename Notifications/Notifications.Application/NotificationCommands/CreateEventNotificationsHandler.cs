using Core.Application;
using Core.Domain.ValueObjects;
using MediatR;
using Notifications.Database.NotificationAggregateDatabase;
using Notifications.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.Application.NotificationCommands
{
    public class CreateEventNotificationsHandler : IRequestHandler<CreateEventNotificationsDto, CommandResult>
    {
        private readonly IMediator mediator;
        private readonly INotificationAggregateRepository repository;

        public CreateEventNotificationsHandler(IMediator mediator, INotificationAggregateRepository repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(CreateEventNotificationsDto request, CancellationToken cancellationToken)
        {
            var notificationExists = await repository.GetByEventId(request.EventId);
            if (notificationExists == null)
            {
                foreach (var email in request.Emails)
                {
                    await mediator.Send(new SendEmailNotificationDto
                    {
                        Title = request.Title,
                        Message = request.Message,
                        Email = email
                    });

                    var notification = NotificationAggregate.Create(
                        new Guid(request.EventId), new Email(email));

                    await repository.Add(notification);
                    await repository.SaveChanges();
                }
            }

            return new CommandResult();
        }
    }
}
