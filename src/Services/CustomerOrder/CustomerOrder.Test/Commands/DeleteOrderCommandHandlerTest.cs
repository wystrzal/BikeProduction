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

        private readonly DeleteOrderCommand command;
        private readonly DeleteOrderCommandHandler commandHandler;
        private readonly List<OrderItem> orderItems;

        public DeleteOrderCommandHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            bus = new Mock<IBus>();
            command = new DeleteOrderCommand(It.IsAny<int>());
            commandHandler = new DeleteOrderCommandHandler(orderRepository.Object, bus.Object);
            orderItems = new List<OrderItem> { new OrderItem(), new OrderItem() };
        }

        [Fact]
        public async Task DeleteOrderCommandHandler_WaitingForConfirmStatus()
        {
            //Arrange
            var order = new Order { OrderStatus = OrderStatus.Waiting_For_Confirm, OrderItems = orderItems };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult(order));

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
            var order = new Order { OrderStatus = OrderStatus.Delivered };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult(order));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            orderRepository.Verify(x => x.Delete(order), Times.Once);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
