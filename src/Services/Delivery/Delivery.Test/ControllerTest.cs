using Delivery.API.Controllers;
using Delivery.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task LoadPack_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<LoadPackCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new DeliveryController(mediator.Object);

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

            var controller = new DeliveryController(mediator.Object);

            //Act
            var action = await controller.LoadPack(It.IsAny<int>(), It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
