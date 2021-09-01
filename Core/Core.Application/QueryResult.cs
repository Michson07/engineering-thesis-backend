using Core.Api;
using Newtonsoft.Json;

namespace Core.Application
{
    public class QueryResult<TResult> : ApiActionResult, IResult
        where TResult : new()
    {
        public TResult? BodyResponse { get; set; } = default!;

        public override string Body => JsonConvert.SerializeObject(BodyResponse);

        public override int Code => BodyResponse != null ? 200 : 404;
    }
}
