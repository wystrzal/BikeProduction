using AutoMapper;
using Common.Application.Messaging;
using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Commands.Handlers;
using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Commands
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<IBus> bus;
        private readonly Mock<IMapper> mapper;

        public CreateOrderCommandHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            bus = new Mock<IBus>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateOrderCommandHandler_ThrowsOrderNotAddedException()
        {
            //Arrange
            var command = new CreateOrderCommand();
            var order = new Order();

            mapper.Setup(x => x.Map<Order>(command)).Returns(order);

            orderRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var commandHandler = new CreateOrderCommandHandler(mapper.Object, orderRepository.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<OrderNotAddedException>(() =>
                commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task CreateOrderCommandHandler_Success()
        {
            //Arrange
            var command = new CreateOrderCommand();
            var order = new Order();

            mapper.Setup(x => x.Map<Order>(command)).Returns(order);

            orderRepository.Setup(x => x.Add(order)).Verifiable();

            orderRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            bus.Setup(x => x.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new CreateOrderCommandHandler(mapper.Object, orderRepository.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            orderRepository.Verify(x => x.Add(order), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
