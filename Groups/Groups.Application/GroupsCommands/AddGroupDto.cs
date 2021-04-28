using Core.Application;
using MediatR;

namespace Groups.Application.GroupsCommands
{
    public class AddGroupDto : IRequest<CommandResult>
    {
        public string OwnerEmail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
