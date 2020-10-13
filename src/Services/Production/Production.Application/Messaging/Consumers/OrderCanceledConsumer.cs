using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Production.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Production.Application.Messaging.Consumers
{
    public class OrderCanceledConsumer : IConsumer<OrderCanceledEvent>
    {
        private readonly IProductionQueueRepo productionQueueRepo;
        private readonly ILogger<OrderCanceledConsumer> logger;

        public OrderCanceledConsumer(IProductionQueueRepo productionQueueRepo, ILogger<OrderCanceledConsumer> logger)
        {
            this.productionQueueRepo = productionQueueRepo;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCanceledEvent> context)
        {
            var productionQueues = await productionQueueRepo.GetByConditionToList(x => x.OrderId == context.Message.OrderId);

            foreach (var productionQueue in productionQueues)
            {
                productionQueueRepo.Delete(productionQueue);
            }

            try
            {
                await productionQueueRepo.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
 
            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}
