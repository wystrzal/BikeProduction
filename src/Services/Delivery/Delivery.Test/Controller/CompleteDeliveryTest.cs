using BikeExtensions;
using Delivery.API.Controllers;
using Delivery.Application.Commands;
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
    public class CompleteDeliveryTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        private readonly DeliveryController controller;

        public CompleteDeliveryTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
            controller = new DeliveryController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task CompleteDelivery_OkResult()
        {
            //Act
            var action = await controller.CompleteDelivery(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<CompleteDeliveryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task CompleteDelivery_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CompleteDeliveryCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.CompleteDelivery(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
