using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IBasketRedisService basketService;

        public OrderCreatedConsumer(IBasketRedisService basketService)
        {
            this.basketService = basketService;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await basketService.RemoveBasket(context.Message.UserId);
        }
    }
}
