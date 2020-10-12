using BikeBaseRepository;
using BikeExtensions;
using Common.Application.Messaging;
using CustomerOrder.Application.Messaging.Consumers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Test.Messaging
{
    public class ChangeOrderStatusConsumerTest
    {
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<ILogger<ChangeOrderStatusConsumer>> logger;

        public ChangeOrderStatusConsumerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            logger = new Mock<ILogger<ChangeOrderStatusConsumer>>();
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_Success()
        {
            //Arrange
            var status = OrderStatus.Confirmed;
            var order = new Order();
            var changeOrderStatusEvent = new ChangeOrderStatusEvent(It.IsAny<int>(), status);

            var context = Mock.Of<ConsumeContext<ChangeOrderStatusEvent>>(x => x.Message == changeOrderStatusEvent);

            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));

            var consumer = new ChangeOrderStatusConsumer(orderRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            Assert.Equal(status, order.OrderStatus);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ChangeOrderStatusConsumer_ThrowsNullDataException()
        {
            //Arrange
            var changeOrderStatusEvent = new ChangeOrderStatusEvent(It.IsAny<int>(), It.IsAny<OrderStatus>());

            var context = Mock.Of<ConsumeContext<ChangeOrderStatusEvent>>(x => x.Message == changeOrderStatusEvent);

            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).ThrowsAsync(new NullDataException());

            var consumer = new ChangeOrderStatusConsumer(orderRepository.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }


        [Fact]
        public async Task ChangeOrderStatusConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var status = OrderStatus.Confirmed;
            var order = new Order();
            var changeOrderStatusEvent = new ChangeOrderStatusEvent(It.IsAny<int>(), status);

            var context = Mock.Of<ConsumeContext<ChangeOrderStatusEvent>>(x => x.Message == changeOrderStatusEvent);

            orderRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(order));
            orderRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Order)));

            var consumer = new ChangeOrderStatusConsumer(orderRepository.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
