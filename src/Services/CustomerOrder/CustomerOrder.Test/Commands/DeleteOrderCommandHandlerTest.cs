using Common.Application.Messaging;
using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Commands.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Test.Commands
{
    public class DeleteOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<IBus> bus;

        public DeleteOrderCommandHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task DeleteOrderCommandHandler_WaitingForConfirmStatus()
        {
            //Arrange
            var orderId = 1;
            var orderItems = new List<OrderItem> { new OrderItem(), new OrderItem() };
            var command = new DeleteOrderCommand(orderId);
            var order = new Order { OrderStatus = OrderStatus.Waiting_For_Confirm, OrderId = orderId, OrderItems = orderItems };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult(order));

            bus.Setup(x => x.Publish(It.IsAny<OrderCanceledEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            orderRepository.Setup(x => x.Delete(order)).Verifiable();

            orderRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new DeleteOrderCommandHandler(orderRepository.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            bus.Verify(x => x.Publish(It.IsAny<OrderCanceledEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            orderRepository.Verify(x => x.Delete(order), Times.Once);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task DeleteOrderCommandHandler_DeliveredStatus()
        {
            //Arrange
            var orderId = 1;
            var command = new DeleteOrderCommand(orderId);
            var order = new Order { OrderStatus = OrderStatus.Delivered, OrderId = orderId };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult(order));

            orderRepository.Setup(x => x.Delete(order)).Verifiable();

            orderRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new DeleteOrderCommandHandler(orderRepository.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            orderRepository.Verify(x => x.Delete(order), Times.Once);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
