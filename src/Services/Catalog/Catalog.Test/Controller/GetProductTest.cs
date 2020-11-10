using BikeExtensions;
using Catalog.API.Controllers;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Controller
{
    public class GetProductTest
    {
        private const string productName = "test";
        private const int productId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        private readonly CatalogController controller;
        private readonly GetProductDto productDto;

        public GetProductTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
            controller = new CatalogController(mediator.Object, logger.Object);
            productDto = new GetProductDto { ProductName = productName };
        }

        [Fact]
        public async Task GetProduct_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productDto));

            //Act
            var action = await controller.GetProduct(productId) as OkObjectResult;
            var value = action.Value as GetProductDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productName, value.ProductName);
        }

        [Fact]
        public async Task GetProduct_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            //Act
            var action = await controller.GetProduct(productId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
