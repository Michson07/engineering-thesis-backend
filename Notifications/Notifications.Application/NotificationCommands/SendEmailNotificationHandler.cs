using Core.Api;
using Core.Application;
using Core.Domain.ValueObjects;
using Core.Services.EmailService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.Application.NotificationCommands
{
    public class SendEmailNotificationHandler : IRequestHandler<SendEmailNotificationDto, CommandResult>
    {
        private readonly IEmailSender emailSender;
        public SendEmailNotificationHandler(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task<CommandResult> Handle(SendEmailNotificationDto request, CancellationToken cancellationToken)
        {
            await emailSender.SendAsync(new Email(request.Email), new EmailMessage(request.Title, request.Message));

            return new CommandResult { Result = new OkResult() };
        }
    }
}
