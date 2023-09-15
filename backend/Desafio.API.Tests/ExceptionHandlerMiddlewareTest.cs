using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.API.Tests
{
    public class ExceptionHandlerMiddlewareTest
    {
        [Fact]
        public async Task ShouldReturnBadRequestWhenSendingInvalidValue()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();
            using var testServer = new TestServer(webBuilder);

            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonSerializer.Serialize(new { valor = 0, prazo = 2 });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/cdb")
            {
                Content = content
            };


            var result = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
