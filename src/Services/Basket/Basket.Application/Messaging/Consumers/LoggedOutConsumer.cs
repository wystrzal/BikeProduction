using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class LoggedOutConsumer : IConsumer<LoggedOutEvent>
    {
        private readonly IBasketRedisService basketRedisService;
        private readonly ILogger<LoggedOutConsumer> logger;

        public LoggedOutConsumer(IBasketRedisService basketRedisService, ILogger<LoggedOutConsumer> logger)
        {
            this.basketRedisService = basketRedisService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<LoggedOutEvent> context)
        {
            ValidateSessionId(context);

            await basketRedisService.RemoveBasket(context.Message.SessionId);
        }

        private void ValidateSessionId(ConsumeContext<LoggedOutEvent> context)
        {
            if (string.IsNullOrWhiteSpace(context.Message.SessionId))
            {
                logger.LogError("SessionID could not be null.");
                throw new ArgumentNullException();
            }
        }
    }
}
