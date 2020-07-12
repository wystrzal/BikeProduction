using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Messaging.Consumers
{
    public class ProductionFinishedConsumer : IConsumer<ProductionFinishedEvent>
    {
        private readonly ICustomerOrderService customerOrderService;
        private readonly IPackToDeliveryRepo packToDeliveryRepo;

        public ProductionFinishedConsumer(ICustomerOrderService customerOrderService, IPackToDeliveryRepo packToDeliveryRepo)
        {
            this.customerOrderService = customerOrderService;
            this.packToDeliveryRepo = packToDeliveryRepo;
        }

        public async Task Consume(ConsumeContext<ProductionFinishedEvent> context)
        {
            var order = await customerOrderService.GetOrder(context.Message.OrderId);

            
        }
    }
}
