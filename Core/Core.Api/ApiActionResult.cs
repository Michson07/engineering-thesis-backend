using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Api
{
    public abstract class ApiActionResult : IActionResult
    {
        public abstract string Body { get; }
        public abstract int Code { get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = Code;
            response.ContentType = "application/json";

            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = Body
            }));
        }

        public override string ToString()
        {
            return Body;
        }
    }
}
