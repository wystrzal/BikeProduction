using Delivery.API.Controllers;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using Delivery.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Controller
{
    public class GetPacksTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;
        private readonly List<GetPacksDto> packsDto;

        public GetPacksTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
            packsDto = new List<GetPacksDto> { new GetPacksDto(), new GetPacksDto() };
        }

        [Fact]
        public async Task GetPacks_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetPacksQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(packsDto));

            //Act
            var action = await controller.GetPacks(It.IsAny<OrderFilteringData>()) as OkObjectResult;
            var value = action.Value as List<GetPacksDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(packsDto.Count, value.Count);
        }
    }
}
