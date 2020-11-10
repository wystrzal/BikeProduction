using BikeExtensions;
using Delivery.API.Controllers;
using Delivery.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Controller
{
    public class DeleteLoadingPlaceTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;

        public DeleteLoadingPlaceTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task DeleteLoadingPlace_OkResult()
        {
            //Act
            var action = await controller.DeleteLoadingPlace(loadingPlaceId) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
            mediator.Verify(x => x.Send(It.IsAny<DeleteLoadingPlaceCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLoadingPlace_BadRequestResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteLoadingPlaceCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.DeleteLoadingPlace(loadingPlaceId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
