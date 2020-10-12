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
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class GetPartTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;

        public GetPartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
        }

        [Fact]
        public async Task GetPart_OkObjectResult()
        {
            //Arrange
            var id = 1;
            var partDto = new GetPartDto() { Id = id };

            mediator.Setup(x => x.Send(It.IsAny<GetPartQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(partDto));

            var controller = new WarehouseController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetPart(id) as OkObjectResult;
            var value = action.Value as GetPartDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(id, value.Id);
        }

        [Fact]
        public async Task GetPart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetPartQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new WarehouseController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetPart(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
