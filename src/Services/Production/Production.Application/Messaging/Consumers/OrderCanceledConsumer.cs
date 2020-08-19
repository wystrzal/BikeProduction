using Common.Application.Messaging;
using MassTransit;
using Production.Core.Interfaces;
using System.Threading.Tasks;

namespace Production.Application.Messaging.Consumers
{
    public class OrderCanceledConsumer : IConsumer<OrderCanceledEvent>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public OrderCanceledConsumer(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }
        public async Task Consume(ConsumeContext<OrderCanceledEvent> context)
        {
            var productionQueues = await productionQueueRepo.GetByConditionToList(x => x.OrderId == context.Message.OrderId);

            foreach (var productionQueue in productionQueues)
            {
                productionQueueRepo.Delete(productionQueue);
            }

            await productionQueueRepo.SaveAllAsync();
        }
    }
}
