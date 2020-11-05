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
    public class GetProductPartsTest
    {
        private const string reference = "1";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;
        private readonly List<GetProductPartsDto> productPartsDto;

        public GetProductPartsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
            productPartsDto = new List<GetProductPartsDto> { new GetProductPartsDto(), new GetProductPartsDto() };
        }

        [Fact]
        public async Task GetProductParts_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductPartsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productPartsDto));

            //Act
            var action = await controller.GetProductParts(reference) as OkObjectResult;
            var value = action.Value as List<GetProductPartsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productPartsDto.Count, value.Count);
        }

        [Fact]
        public void GetProductParts_NullReference_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetProductPartsQuery(It.IsAny<string>()));
        }
    }
}
