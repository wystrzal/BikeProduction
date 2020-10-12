using BikeExtensions;
using Catalog.API.Controllers;
using Catalog.Application.Commands;
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
    public class UpdateProductTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        public UpdateProductTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
        }

        [Fact]
        public async Task UpdateProduct_OkResult()
        {
            //Arrange
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.UpdateProduct(It.IsAny<UpdateProductCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_BadRequestObjectResult()
        {
            //Arrange
            var controller = new CatalogController(mediator.Object, logger.Object);

            mediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.UpdateProduct(It.IsAny<UpdateProductCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
