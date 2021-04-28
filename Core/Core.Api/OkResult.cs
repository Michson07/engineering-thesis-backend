namespace Core.Api
{
    public class OkResult : ApiActionResult
    {
        public override string Body => "Ok";

        public override int Code => 200;
    }
}
