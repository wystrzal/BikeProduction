using BaseRepository.Exceptions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
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
            try
            {
                ValidateContext(context);

                var productionQueues = await GetProductionQueues(context.Message.OrderId);

                await DeleteProductionQueues(productionQueues);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void ValidateContext(ConsumeContext<OrderCanceledEvent> context)
        {
            if (context.Message.OrderId <= 0)
            {
                throw new ArgumentException("OrderID must be greater than zero.");
            }
        }

        private async Task<List<ProductionQueue>> GetProductionQueues(int orderId)
        {
            var productionQueues = await productionQueueRepo.GetByConditionToList(x => x.OrderId == orderId);

            if (productionQueues.Count <= 0)
            {
                throw new NullDataException();
            }

            return productionQueues;
        }

        private async Task DeleteProductionQueues(List<ProductionQueue> productionQueues)
        {
            foreach (var productionQueue in productionQueues)
            {
                productionQueueRepo.Delete(productionQueue);
            }

            await productionQueueRepo.SaveAllAsync();
        }
    }
}
