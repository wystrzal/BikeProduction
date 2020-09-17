using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductionConfirmedConsumer : IConsumer<ProductionConfirmedEvent>
    {
        private readonly IProductPartRepo productPartRepo;
        private readonly ILogger<ProductionConfirmedConsumer> logger;

        public ProductionConfirmedConsumer(IProductPartRepo productPartRepo, ILogger<ProductionConfirmedConsumer> logger)
        {
            this.productPartRepo = productPartRepo;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductionConfirmedEvent> context)
        {
            var parts = await productPartRepo.GetPartsForCheckAvailability(context.Message.Reference);
            int productionQuantity = context.Message.Quantity;

            bool startProduction = false;

            foreach (var part in parts)
            {
                if (part.Quantity < (productionQuantity * part.QuantityForProduction))
                {
                    startProduction = false;
                    break;
                }
                else
                {
                    startProduction = true;

                    part.Quantity -= (productionQuantity * part.QuantityForProduction);
                }
            }

            await productPartRepo.SaveAllAsync();

            await context.RespondAsync<ProductionConfirmedResult>(new { StartProduction = startProduction });

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}
