using Core.Application;
using MediatR;

namespace Groups.Application.AnnouncementCommands
{
    public class AddAnnouncementDto : IRequest<CommandResult>
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string CreatorEmail { get; set; } = string.Empty;
    }
}
