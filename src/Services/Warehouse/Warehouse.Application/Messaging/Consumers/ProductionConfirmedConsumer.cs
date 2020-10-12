using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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
            if (string.IsNullOrWhiteSpace(context.Message.Reference))
            {
                logger.LogError("Reference could not be null.");
                throw new ArgumentNullException();
            }

            var parts = await productPartRepo.GetPartsForProduction(context.Message.Reference);

            try
            {
                await context.RespondAsync<ProductionConfirmedResult>(new
                {
                    ConfirmProduction = await ConfirmProductionIfPartsAvailable(parts, context.Message.Quantity)
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task<bool> ConfirmProductionIfPartsAvailable(List<Part> parts, int productionQuantity)
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

            if (confirmProduction)
                await productPartRepo.SaveAllAsync();

            return confirmProduction;
        }
    }
}
