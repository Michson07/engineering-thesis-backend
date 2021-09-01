namespace Core.Api
{
    public class ConflictResult : ApiActionResult
    {
        private readonly string value;

        public ConflictResult(string value)
        {
            this.value = value;
        }

        public override string Body => $"{value}";

        public override int Code => 409;
    }
}
