using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using StarWars.Api;
using Xunit;

namespace StarWars.Tests.Integration.Api.Controllers
{
    public class GraphQLControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GraphQLControllerShould()
        {
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Test").UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ReturnR2D2Droid()
        {
            const string query = @"{
                ""query"": ""query { hero { id name } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("R2-D2", responseString);
        }
    }
}
