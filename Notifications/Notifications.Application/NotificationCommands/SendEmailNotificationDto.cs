using Core.Application;
using MediatR;

namespace Notifications.Application.NotificationCommands
{
    public class SendEmailNotificationDto : IRequest<CommandResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
