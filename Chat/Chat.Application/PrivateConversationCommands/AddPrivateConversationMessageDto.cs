using Core.Application;
using MediatR;

namespace Chat.Application.PrivateConversationCommands
{
    public class AddPrivateConversationMessageDto : IRequest<CommandResult>
    {
        public string Message { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
    }
}
