using AutoMapper;
using Common.Application.Messaging;
using CustomerOrder.Application.Commands;
using CustomerOrder.Application.Commands.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using Moq;
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

        private readonly CreateOrderCommand command;
        private readonly CreateOrderCommandHandler commandHandler;
        private readonly Order order;

        public CreateOrderCommandHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            bus = new Mock<IBus>();
            mapper = new Mock<IMapper>();
            command = new CreateOrderCommand();
            commandHandler = new CreateOrderCommandHandler(mapper.Object, orderRepository.Object, bus.Object);
            order = new Order();
        }

        [Fact]
        public async Task CreateOrderCommandHandler_Success()
        {
            //Arrange
            mapper.Setup(x => x.Map<Order>(command)).Returns(order);

            orderRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            orderRepository.Verify(x => x.Add(order), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
