using BikeExtensions;
using Catalog.API.Controllers;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Controller
{
    public class GetProductTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        public GetProductTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
        }

        [Fact]
        public async Task GetProduct_OkObjectResult()
        {
            //Arrange
            var productName = "test";
            var productDto = new GetProductDto { ProductName = productName };

            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productDto));

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProduct(It.IsAny<int>()) as OkObjectResult;
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

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProduct(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
