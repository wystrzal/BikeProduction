using Basket.Application.Messaging.Consumers;
using Basket.Core.Dtos;
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
    public class LoggedInConsumerTest
    {
        private const string userId = "1";
        private const string sessionId = "1";

        private readonly Mock<IBasketRedisService> basketRedisService;
        private readonly Mock<ILogger<LoggedInConsumer>> logger;

        private readonly LoggedInConsumer consumer;
        private readonly UserBasketDto basketDto;

        public LoggedInConsumerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            logger = new Mock<ILogger<LoggedInConsumer>>();
            consumer = new LoggedInConsumer(basketRedisService.Object, logger.Object);
            basketDto = new UserBasketDto();
        }

        [Fact]
        public async Task LoggedInConsumer_NullUserId_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task LoggedInConsumer_NullSessionId_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(sessionId: null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task LoggedInConsumer_Success()
        {
            //Arrange
            var context = GetContext();
            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(basketDto));

            //Act
            await consumer.Consume(context);

            //Assert
            basketRedisService.Verify(x => x.SaveBasket(userId, basketDto), Times.Once);
        }

        private ConsumeContext<LoggedInEvent> GetContext(string userId = userId, string sessionId = sessionId)
        {
            var loggedInEvent = new LoggedInEvent { UserId = userId, SessionId = sessionId };

            return Mock.Of<ConsumeContext<LoggedInEvent>>(x => x.Message == loggedInEvent);
        }
    }
}
