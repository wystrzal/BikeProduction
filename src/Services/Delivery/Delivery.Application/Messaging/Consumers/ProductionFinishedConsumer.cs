using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Messaging.Consumers
{
    public class ProductionFinishedConsumer : IConsumer<ProductionFinishedEvent>
    {
        private readonly ICustomerOrderService customerOrderService;
        private readonly IPackToDeliveryRepo packToDeliveryRepo;
        private readonly ILogger<ProductionFinishedConsumer> logger;

        public ProductionFinishedConsumer(ICustomerOrderService customerOrderService, IPackToDeliveryRepo packToDeliveryRepo,
            ILogger<ProductionFinishedConsumer> logger)
        {
            this.customerOrderService = customerOrderService;
            this.packToDeliveryRepo = packToDeliveryRepo;
            this.logger = logger;
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
                    PackStatus = PackStatus.Waiting
                };

                packToDeliveryRepo.Add(newPackToDelivery);
            }
            else
            {
                packToDelivery.ProductsQuantity += context.Message.ProductsQuantity;
            }

            await packToDeliveryRepo.SaveAllAsync();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}
