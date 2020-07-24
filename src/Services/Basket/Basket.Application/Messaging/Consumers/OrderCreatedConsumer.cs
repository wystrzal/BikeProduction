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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBasketService basketService;

        public OrderCreatedConsumer(IHttpContextAccessor httpContextAccessor, IBasketService basketService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.basketService = basketService;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();

            await basketService.RemoveBasket(userId);
        }
    }
}
