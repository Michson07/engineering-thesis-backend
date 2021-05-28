using Core.Application;
using MediatR;

namespace Groups.Application.AnnouncementCommands
{
    public class UpdateAnnouncementDto : IRequest<CommandResult>
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
