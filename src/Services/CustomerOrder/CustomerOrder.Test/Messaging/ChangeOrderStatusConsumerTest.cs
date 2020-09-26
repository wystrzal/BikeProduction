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
            var id = 1;
            var status = OrderStatus.Confirmed;
            var order = new Order { OrderId = id };
            var changeOrderStatusEvent = new ChangeOrderStatusEvent(id, status);

            var context = Mock.Of<ConsumeContext<ChangeOrderStatusEvent>>(x => x.Message == changeOrderStatusEvent);

            orderRepository.Setup(x => x.GetById(id)).Returns(Task.FromResult(order));
            orderRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new ChangeOrderStatusConsumer(orderRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            Assert.Equal(status, order.OrderStatus);
            orderRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
