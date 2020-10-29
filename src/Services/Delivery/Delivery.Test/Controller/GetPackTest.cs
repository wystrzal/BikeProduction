using BikeExtensions;
using Delivery.API.Controllers;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Controller
{
    public class GetPackTest
    {
        private const int packId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;
        private readonly GetPackDto packDto;

        public GetPackTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
            packDto = new GetPackDto { Id = packId };
        }

        [Fact]
        public async Task GetPack_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetPackQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(packDto));

            //Act
            var action = await controller.GetPack(packId) as OkObjectResult;
            var value = action.Value as GetPackDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(packId, value.Id);
        }

        [Fact]
        public async Task GetPack_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetPackQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.GetPack(packId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
