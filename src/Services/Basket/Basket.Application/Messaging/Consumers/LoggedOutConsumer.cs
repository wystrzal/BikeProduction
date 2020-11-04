using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class LoggedOutConsumer : IConsumer<LoggedOutEvent>
    {
        private readonly IBasketRedisService basketRedisService;

        public LoggedOutConsumer(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task Consume(ConsumeContext<LoggedOutEvent> context)
        {
            await basketRedisService.RemoveBasket(context.Message.SessionId);
        }
    }
}
