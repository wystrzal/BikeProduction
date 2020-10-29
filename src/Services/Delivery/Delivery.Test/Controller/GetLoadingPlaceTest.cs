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
    public class GetLoadingPlaceTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;
        private readonly GetLoadingPlaceDto loadingPlaceDto;

        public GetLoadingPlaceTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
            loadingPlaceDto = new GetLoadingPlaceDto { Id = loadingPlaceId };
        }

        [Fact]
        public async Task GetLoadingPlace_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetLoadingPlaceQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(loadingPlaceDto));

            //Act
            var action = await controller.GetLoadingPlace(loadingPlaceId) as OkObjectResult;
            var value = action.Value as GetLoadingPlaceDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(loadingPlaceId, value.Id);
        }

        [Fact]
        public async Task GetLoadingPlace_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetLoadingPlaceQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            //Act
            var action = await controller.GetLoadingPlace(loadingPlaceId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
