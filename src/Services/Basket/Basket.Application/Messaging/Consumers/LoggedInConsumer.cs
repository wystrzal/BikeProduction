using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class LoggedInConsumer : IConsumer<LoggedInEvent>
    {
        private readonly IBasketRedisService basketRedisService;

        public LoggedInConsumer(IBasketRedisService basketRedisService)
        {
            this.basketRedisService = basketRedisService;
        }

        public async Task Consume(ConsumeContext<LoggedInEvent> context)
        {
            var sessionBasket = await basketRedisService.GetBasket(context.Message.SessionId);

            if (sessionBasket == null)
            {
                return;
            }

            await MergeSessionBasketWithUserBasket(context.Message.UserId, sessionBasket);

            await basketRedisService.SaveBasket(context.Message.UserId, sessionBasket);
        }

        private async Task MergeSessionBasketWithUserBasket(string userId, UserBasketDto sessionBasket)
        {
            var userBasket = await basketRedisService.GetBasket(userId);

            if (userBasket != null)
            {
                sessionBasket.Products.AddRange(userBasket.Products);
                sessionBasket.TotalPrice += userBasket.TotalPrice;
            }
        }
    }
}
