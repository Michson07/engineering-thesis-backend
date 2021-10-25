using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Core.Api
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string? GetPrincipalEmail()
        {
            if (!Request.Headers.TryGetValue("Authorization", out StringValues token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var decoded = handler
                .ReadJwtToken(
                    token
                    .First()
                    .Split(" ")
                    .Last()
                );
            return decoded.Claims.Single(claim => claim.Type == "emails").Value;
        }
    }
}
