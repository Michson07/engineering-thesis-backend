namespace Core.Api
{
    public abstract class ApiActionResult
    {
        public abstract string Result { get; }

        public override string ToString()
        {
            return Result;
        }
    }
}
