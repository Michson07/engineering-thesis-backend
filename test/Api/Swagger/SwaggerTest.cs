using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Swagger
{
    public class SwaggerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public SwaggerTest(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ShouldGenerateSwaggerPage()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/swagger/index.html");

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
