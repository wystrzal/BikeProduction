using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class GetStoragePlacesTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;

        public GetStoragePlacesTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
        }

        [Fact]
        public async Task GetStoragePlaces_OkResultObject()
        {
            //Arrange
            IEnumerable<GetStoragePlacesDto> storagePlacesDto = new List<GetStoragePlacesDto> 
                { new GetStoragePlacesDto(), new GetStoragePlacesDto() };

            mediator.Setup(x => x.Send(It.IsAny<GetStoragePlacesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(storagePlacesDto));

            var controller = new WarehouseController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetStoragePlaces() as OkObjectResult;
            var value = action.Value as List<GetStoragePlacesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(storagePlacesDto.Count(), value.Count);
        }
    }
}
