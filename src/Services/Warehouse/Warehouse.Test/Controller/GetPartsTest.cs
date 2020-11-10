using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class GetPartsTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;
        private readonly IEnumerable<GetPartsDto> partsDto;

        public GetPartsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
            partsDto = new List<GetPartsDto> { new GetPartsDto(), new GetPartsDto() };
        }

        [Fact]
        public async Task GetParts_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetPartsQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(partsDto));

            //Act
            var action = await controller.GetParts() as OkObjectResult;
            var value = action.Value as List<GetPartsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(partsDto.Count(), value.Count);
        }
    }
}
