using Core.Api;

namespace Core.Application
{
    public class CommandResult : IResult
    {
        public ApiActionResult Result { get; set; } = null!;
    }
}
