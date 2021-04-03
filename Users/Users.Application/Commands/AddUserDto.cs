using Core.Application;
using MediatR;

namespace Users.Application.Commands
{
    public class AddUserDto : IRequest<CommandResult>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
