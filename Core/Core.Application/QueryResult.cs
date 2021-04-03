namespace Core.Application
{
    public class QueryResult<TResult> where TResult : new()
    {
        public TResult? Body { get; set; }
    }
}
