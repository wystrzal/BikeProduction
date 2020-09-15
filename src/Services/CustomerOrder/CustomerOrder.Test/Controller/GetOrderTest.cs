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
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CustomerOrderController>> logger;

        public GetOrderTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CustomerOrderController>>();
        }

        [Fact]
        public async Task GetOrder_OkObjectResult()
        {
            //Arrange
            int orderId = 1;

            mediator.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetOrderDto { OrderId = orderId }));

            var controller = new CustomerOrderController(mediator.Object, logger.Object);

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
            int orderId = 1;

            mediator.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new CustomerOrderController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetOrder(orderId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
