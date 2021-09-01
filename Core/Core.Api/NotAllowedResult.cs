using Newtonsoft.Json;

namespace Core.Api
{
    public class NotAllowedResult<T, Y> : ApiActionResult
    {
        private readonly T who;
        private readonly Y where;

        public NotAllowedResult(T who, Y where)
        {
            this.who = who;
            this.where = where;
        }

        public override string Body => $"{JsonConvert.SerializeObject(who)} not allowed to act with {JsonConvert.SerializeObject(where)}.";

        public override int Code => 405;
    }
}
