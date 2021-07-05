using Core.Application;
using MediatR;

namespace Chat.Application.GroupConversationCommands
{
    public class AddGroupConversationMessageDto : IRequest<CommandResult>
    {
        public string Message { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
    }
}
