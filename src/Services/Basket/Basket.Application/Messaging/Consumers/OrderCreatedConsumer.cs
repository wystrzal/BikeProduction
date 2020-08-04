using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IBasketService basketService;

        public OrderCreatedConsumer(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await basketService.RemoveBasket(context.Message.UserId);
        }
    }
}
