using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

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
            var parts = await productPartRepo.GetPartsForProduction(context.Message.Reference);
            int productionQuantity = context.Message.Quantity;

            bool confirmProduction = ConfirmProductionIfPartsAvailable(parts, productionQuantity);

            if (confirmProduction)
                await productPartRepo.SaveAllAsync();

            await context.RespondAsync<ProductionConfirmedResult>(new { ConfirmProduction = confirmProduction });

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private bool ConfirmProductionIfPartsAvailable(List<Part> parts, int productionQuantity)
        {
            bool confirmProduction = true;

            foreach (var part in parts)
            {
                if (part.Quantity < (productionQuantity * part.QuantityForProduction))
                {
                    confirmProduction = false;
                    break;
                }
                else
                {
                    part.Quantity -= (productionQuantity * part.QuantityForProduction);
                }
            }

            return confirmProduction;
        }
    }
}
