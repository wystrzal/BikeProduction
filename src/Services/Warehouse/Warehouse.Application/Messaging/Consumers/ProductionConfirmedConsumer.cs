using Common.Application.Messaging;
using MassTransit;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductionConfirmedConsumer : IConsumer<ProductionConfirmedEvent>
    {
        private readonly IProductPartRepo productPartRepo;

        public ProductionConfirmedConsumer(IProductPartRepo productPartRepo)
        {
            this.productPartRepo = productPartRepo;
        }

        public async Task Consume(ConsumeContext<ProductionConfirmedEvent> context)
        {
            var parts = await productPartRepo.GetPartsForCheckAvailability(context.Message.Reference);
            int productionQuantity = context.Message.Quantity;

            bool startProduction = false;

            foreach (var part in parts)
            {
                if (part.PartName.ToLower() == "circle" && part.Quantity < (productionQuantity * 2)
                    || part.Quantity < productionQuantity)
                {
                    startProduction = false;
                    break;
                }
                else
                {
                    startProduction = true;
                    if (part.PartName.ToLower() == "circle")
                    {
                        part.Quantity -= (productionQuantity * 2);
                    }
                    else
                    {
                        part.Quantity -= (productionQuantity);
                    }             
                }
            }

            await productPartRepo.SaveAllAsync();
            
            await context.RespondAsync<ProductionConfirmedResult>(new { StartProduction = startProduction });
        }
    }
}
