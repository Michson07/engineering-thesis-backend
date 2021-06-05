namespace Core.Api
{
    public class NoContentResult : ApiActionResult
    {
        public override string Body => "NoContent";

        public override int Code => 204;
    }
}
