using Castle.Core.Logging;
using Delivery.API.Controllers;
using Delivery.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<DeliveryController>> logger;

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<DeliveryController>>();
        }

        [Fact]
        public async Task LoadPack_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<LoadPackCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new DeliveryController(mediator.Object, logger.Object);

            //Act
            var action = await controller.LoadPack(It.IsAny<int>(), It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<LoadPackCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task LoadPack_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<LoadPackCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new DeliveryController(mediator.Object, logger.Object);

            //Act
            var action = await controller.LoadPack(It.IsAny<int>(), It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task StartDelivery_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<StartDeliveryCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new DeliveryController(mediator.Object, logger.Object);

            //Act
            var action = await controller.StartDelivery(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<StartDeliveryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task StartDelivery_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<StartDeliveryCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new DeliveryController(mediator.Object, logger.Object);

            //Act
            var action = await controller.StartDelivery(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CompleteDelivery_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CompleteDeliveryCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new DeliveryController(mediator.Object, logger.Object);

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

            var controller = new DeliveryController(mediator.Object, logger.Object);

            //Act
            var action = await controller.CompleteDelivery(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
