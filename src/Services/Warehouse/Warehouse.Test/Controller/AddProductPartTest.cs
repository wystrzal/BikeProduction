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
    public class AddProductPartTest
    {
        private const int id = 1;
        private const string reference = "1";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;

        public AddProductPartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task AddProductPart_OkResult()
        {
            //Act
            var action = await controller.AddProductPart(reference, id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<AddProductPartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task AddProductPart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<AddProductPartCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.AddProductPart(reference, id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public void AddProductPart_PartIdEqualZero_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new AddProductPartCommand(It.IsAny<int>(), reference));
        }

        [Fact]
        public void AddProductPart_NullReference_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new AddProductPartCommand(id, It.IsAny<string>()));
        }
    }
}
