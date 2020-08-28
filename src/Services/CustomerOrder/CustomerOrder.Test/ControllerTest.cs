using CustomerOrder.API.Controllers;
using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task CreateOrder_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new CustomerOrderController(mediator.Object);

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

            var controller = new CustomerOrderController(mediator.Object);

            //Act
            var action = await controller.CreateOrder(It.IsAny<CreateOrderCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetOrder_OkObjectResult()
        {
            //Arrange
            int orderId = 1;

            mediator.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetOrderDto { OrderId = orderId }));

            var controller = new CustomerOrderController(mediator.Object);

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

            var controller = new CustomerOrderController(mediator.Object);

            //Act
            var action = await controller.GetOrder(orderId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }



    }
}
