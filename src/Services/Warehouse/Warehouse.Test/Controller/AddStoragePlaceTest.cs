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
    public class AddStoragePlaceTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;

        public AddStoragePlaceTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
        }

        [Fact]
        public async Task AddStoragePlace_OkResult()
        {
            //Arrange
            var controller = new WarehouseController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddStoragePlace(It.IsAny<AddStoragePlaceCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<AddStoragePlaceCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
