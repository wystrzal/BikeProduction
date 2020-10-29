using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.Application.Commands;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class UpdatePartTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;

        public UpdatePartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task UpdatePart_OkResult()
        {
            //Act
            var action = await controller.UpdatePart(It.IsAny<UpdatePartCommand>()) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
            mediator.Verify(x => x.Send(It.IsAny<UpdatePartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<UpdatePartCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.UpdatePart(It.IsAny<UpdatePartCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
