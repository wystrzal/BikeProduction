using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
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
            int orderId = context.Message.OrderId;

            var packToDelivery = await packToDeliveryRepo.GetByConditionFirst(x => x.OrderId == orderId);

            if (packToDelivery == null)
            {
                var order = await customerOrderService.GetOrder(orderId);
                var newPackToDelivery = new PackToDelivery
                {
                    OrderId = orderId,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    ProductsQuantity = context.Message.ProductsQuantity,
                };

                packToDeliveryRepo.Add(newPackToDelivery);
            }
            else
            {
                packToDelivery.ProductsQuantity += context.Message.ProductsQuantity;
            }

            await packToDeliveryRepo.SaveAllAsync();         
        }
    }
}
