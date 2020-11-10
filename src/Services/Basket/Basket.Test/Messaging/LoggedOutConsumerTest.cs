using Basket.Application.Messaging.Consumers;
using Basket.Core.Interfaces;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Messaging
{
    public class LoggedOutConsumerTest
    {
        private const string sessionId = "1";

        private readonly Mock<IBasketRedisService> basketRedisService;
        private readonly Mock<ILogger<LoggedOutConsumer>> logger;

        private readonly LoggedOutConsumer consumer;

        public LoggedOutConsumerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            logger = new Mock<ILogger<LoggedOutConsumer>>();
            consumer = new LoggedOutConsumer(basketRedisService.Object, logger.Object);
        }

        [Fact]
        public async Task LoggedOutConsumer_NullSessionId_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task LoggedOutConsumer_Success()
        {
            //Arrange
            var context = GetContext();

            //Act
            await consumer.Consume(context);

            //Assert
            basketRedisService.Verify(x => x.RemoveBasket(sessionId), Times.Once);
        }

        private ConsumeContext<LoggedOutEvent> GetContext(string sessionId = sessionId)
        {
            var loggedOutEvent = new LoggedOutEvent { SessionId = sessionId };

            return Mock.Of<ConsumeContext<LoggedOutEvent>>(x => x.Message == loggedOutEvent);
        }
    }
}
