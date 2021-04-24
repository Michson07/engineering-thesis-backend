namespace Core.Api
{
    public class ConflictResult<T> : ApiActionResult
    {
        private readonly T value;
        public ConflictResult(T value)
        {
            this.value = value;
        }

        public override string Result => $"{value} already exists.";
    }
}
