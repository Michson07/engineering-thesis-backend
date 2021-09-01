namespace Core.Api
{
    public class ApplicationErrorResult : ApiActionResult
    {
        public override string Body => "Wystąpił niespodziewany błąd";

        public override int Code => 500;
    }
}
