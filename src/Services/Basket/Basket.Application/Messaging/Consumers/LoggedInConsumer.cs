using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var userBasket = await basketRedisService.GetBasket(context.Message.UserId);

            MergeProducts(sessionBasket, userBasket);

            await basketRedisService.SaveBasket(context.Message.UserId, sessionBasket);
        }

        private void MergeProducts(UserBasketDto sessionBasket, UserBasketDto userBasket)
        {
            foreach (var sessionProduct in sessionBasket.Products)
            {
                var userProduct = userBasket.Products.Where(x => x.Id == sessionProduct.Id).FirstOrDefault();

                if (userProduct == null)
                {
                    continue;
                }

                sessionProduct.Quantity += userProduct.Quantity;
                sessionProduct.Price += userProduct.Price;
            }
        }
    }
}
