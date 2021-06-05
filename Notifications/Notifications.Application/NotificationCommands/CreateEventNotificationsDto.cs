using Core.Application;
using MediatR;
using System.Collections.Generic;

namespace Notifications.Application.NotificationCommands
{
    public class CreateEventNotificationsDto : IRequest<CommandResult>
    {
        public string EventId { get; set; } = string.Empty;
        public IEnumerable<string> Emails { get; set; } = new List<string>();
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
