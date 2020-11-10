using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.Application.Commands;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class DeletePartTest
    {
        private const int id = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;

        private readonly WarehouseController controller;
        private readonly DeletePartCommand command;

        public DeletePartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
            command = new DeletePartCommand(id);
        }

        [Fact]
        public async Task DeletePart_OkResult()
        {
            //Act
            var action = await controller.DeletePart(id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<DeletePartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }


        [Fact]
        public async Task DeletePart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeletePartCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.DeletePart(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(action.Value);
            Assert.Equal(400, action.StatusCode);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public void DeletePartCommand_PartIdEqualZero_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new DeletePartCommand(It.IsAny<int>()));
        }
    }
}
