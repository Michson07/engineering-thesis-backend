using Core.Application;
using MediatR;

namespace Chat.Application.PrivateConversationCommands
{
    public class AddPrivateConversationMessageDto : IRequest<CommandResult>
    {
        public string Message { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string RecipientId { get; set; } = string.Empty;
    }
}
