using Core.Application;
using MediatR;

namespace Users.Application.Commands
{
    public class AddUserDto : IRequest<CommandResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
