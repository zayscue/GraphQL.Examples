using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Api.Controllers;
using StarWars.Api.Models;
using Xunit;

namespace StarWars.Tests.Unit.Api.Controllers
{
    public class GraphQLControllerShould
    {
        private readonly GraphQLController _graphqlController;

        public GraphQLControllerShould()
        {
            var documentExecutor = new Mock<IDocumentExecuter>();
            documentExecutor.Setup(x => x.ExecuteAsync(It.IsAny<ExecutionOptions>()))
                .Returns(Task.FromResult(new ExecutionResult()));
            var schema = new Mock<ISchema>();
            var logger = new Mock<ILogger<GraphQLController>>();
            _graphqlController = new GraphQLController(documentExecutor.Object, schema.Object, logger.Object);
        }

        [Fact]
        public async void ReturnNotNullExecutionResult()
        {
            var query = new GraphQLQuery {Query = @"{ ""query"": ""query { hero { id name } }"""};
            var result = await _graphqlController.Post(query);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var executionResult = okObjectResult.Value;
            Assert.NotNull(executionResult);
        }
    }
}
