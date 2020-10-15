using BikeExtensions;
using CustomerOrder.API.Controllers;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
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
    public class GetOrderTest
    {
        private const int orderId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CustomerOrderController>> logger;

        private readonly CustomerOrderController controller;
        private readonly GetOrderDto orderDto;

        public GetOrderTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CustomerOrderController>>();
            controller = new CustomerOrderController(mediator.Object, logger.Object);
            orderDto = new GetOrderDto { OrderId = orderId };
        }

        [Fact]
        public async Task GetOrder_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(orderDto));

            //Act
            var action = await controller.GetOrder(orderId) as OkObjectResult;
            var value = action.Value as GetOrderDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(orderId, value.OrderId);
        }

        [Fact]
        public async Task GetOrder_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            //Act
            var action = await controller.GetOrder(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
