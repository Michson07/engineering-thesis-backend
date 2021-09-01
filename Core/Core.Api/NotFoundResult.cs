namespace Core.Api
{
    public class NotFoundResult : ApiActionResult
    {
        private readonly string message;

        public NotFoundResult(string message)
        {
            this.message = message;
        }

        public override string Body => message;

        public override int Code => 404;
    }
}
