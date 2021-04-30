namespace Core.Api
{
    public class NotFoundResult<T> : ApiActionResult
    {
        private readonly T value;
        public NotFoundResult(T value)
        {
            this.value = value;
        }

        public override string Body => $"{value} not found.";

        public override int Code => 404;
    }
}
