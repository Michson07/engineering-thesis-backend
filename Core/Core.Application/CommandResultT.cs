using Core.Api;

namespace Core.Application
{
    public class CommandResult<T> where T : ResponseView
    {
        public ApiActionResult Result { get; set; } = null!;
        public T Body { get; set; } = null!;
    }
}
