using CustomerOrder.API.Controllers;
using CustomerOrder.Application.Commands;
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

namespace CustomerOrder.Test.Controller
{
    public class CreateOrderTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CustomerOrderController>> logger;

        public CreateOrderTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CustomerOrderController>>();
        }

        [Fact]
        public async Task CreateOrder_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new CustomerOrderController(mediator.Object, logger.Object);

            //Act
            var action = await controller.CreateOrder(It.IsAny<CreateOrderCommand>()) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
            mediator.Verify(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new CustomerOrderController(mediator.Object, logger.Object);

            //Act
            var action = await controller.CreateOrder(It.IsAny<CreateOrderCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
