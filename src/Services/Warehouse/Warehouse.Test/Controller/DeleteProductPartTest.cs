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
    public class DeleteProductPartTest
    {
        private const int id = 1;
        private const string reference = "1";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;

        public DeleteProductPartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task DeleteProductPart_OkResult()
        {
            //Act
            var action = await controller.DeleteProductPart(reference, id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<DeleteProductPartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteProductPart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteProductPartCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.DeleteProductPart(reference, id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public void DeleteProductPart_PartIdEqualZero_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new DeleteProductPartCommand(It.IsAny<int>(), reference));
        }

        [Fact]
        public void DeleteProductPart_NullReference_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteProductPartCommand(id, It.IsAny<string>()));
        }
    }
}
