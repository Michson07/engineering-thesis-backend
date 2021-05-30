using Core.Application;
using MediatR;

namespace Groups.Application.GroupsCommands
{
    public class JoinGroupDto : IRequest<CommandResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
