using Core.Application;
using MediatR;

namespace Groups.Application.ResourcesCommands
{
    public class AddResourceDto : IRequest<CommandResult>
    {
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public byte[] Value { get; set; } = null!;
    }
}
