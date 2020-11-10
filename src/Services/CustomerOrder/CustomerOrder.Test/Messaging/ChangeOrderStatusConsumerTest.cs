using BaseRepository.Exceptions;
using BikeExtensions;
using Common.Application.Messaging;
using CustomerOrder.Application.Messaging.Consumers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Test.Messaging
{
    public class ChangeOrderStatusConsumerTest
    {
        private const OrderStatus orderStatus = OrderStatus.Confirmed;
        private const int orderId = 1;

        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<ILogger<ChangeOrderStatusConsumer>> logger;

        private readonly ChangeOrderStatusConsumer consumer;
        private readonly ConsumeContext<ChangeOrderStatusEvent> context;
        private readonly Order order;

        public ChangeOrderStatusConsumerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            logger = new Mock<ILogger<ChangeOrderStatusConsumer>>();
            consumer = new ChangeOrderStatusConsumer(orderRepository.Object, logger.Object);
            context = GetContext();
            order = new Order();
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_Success()
        {
            //Arrange
            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));

            //Act
            await consumer.Consume(context);

            //Assert
            Assert.Equal(orderStatus, order.OrderStatus);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_OrderIdEqualZero_ThrowsArgumentException()
        {
            //Arrange
            var context = GetContext(It.IsAny<int>());
            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_OrderStatusEqualZero_ThrowsArgumentException()
        {
            //Arrange
            var context = GetContext(orderStatus: It.IsAny<OrderStatus>());
            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_ThrowsNullDataException()
        {
            //Arrange
            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).ThrowsAsync(new NullDataException());

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }


        [Fact]
        public async Task ChangeOrderStatusConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));
            orderRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Order)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<ChangeOrderStatusEvent> GetContext(int orderId = orderId, OrderStatus orderStatus = orderStatus)
        {
            var changeOrderStatusEvent = new ChangeOrderStatusEvent(orderId, orderStatus);
            return Mock.Of<ConsumeContext<ChangeOrderStatusEvent>>(x => x.Message == changeOrderStatusEvent);
        }
    }
}
