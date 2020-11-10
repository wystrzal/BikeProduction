using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class LoggedInConsumer : IConsumer<LoggedInEvent>
    {
        private readonly IBasketRedisService basketRedisService;
        private readonly ILogger<LoggedInConsumer> logger;

        public LoggedInConsumer(IBasketRedisService basketRedisService, ILogger<LoggedInConsumer> logger)
        {
            this.basketRedisService = basketRedisService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<LoggedInEvent> context)
        {
            ValidateContext(context);

            var sessionBasket = await basketRedisService.GetBasket(context.Message.SessionId);
            var userBasket = await basketRedisService.GetBasket(context.Message.UserId);

            MergeProducts(sessionBasket, userBasket);

            await basketRedisService.SaveBasket(context.Message.UserId, userBasket);
        }

        private void ValidateContext(ConsumeContext<LoggedInEvent> context)
        {
            if (string.IsNullOrWhiteSpace(context.Message.UserId))
            {
                logger.LogError("UserId could not be null.");
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(context.Message.SessionId))
            {
                logger.LogError("SessionId could not be null.");
                throw new ArgumentNullException();
            }
        }

        private void MergeProducts(UserBasketDto sessionBasket, UserBasketDto userBasket)
        {
            foreach (var sessionProduct in sessionBasket.Products)
            {
                var userProduct = userBasket.Products.Where(x => x.Id == sessionProduct.Id).FirstOrDefault();

                if (userProduct == null)
                {
                    userBasket.Products.Add(sessionProduct);
                }
                else
                {
                    userProduct.Quantity += sessionProduct.Quantity;
                    userProduct.Price += sessionProduct.Price;
                }

                userBasket.TotalPrice += sessionProduct.Price;
            }
        }
    }
}
