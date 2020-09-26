using BikeBaseRepository;
using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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
            PackToDelivery packToDelivery = null;
            try
            {
                packToDelivery = await packToDeliveryRepo.GetByConditionFirst(x => x.OrderId == context.Message.OrderId);
                packToDelivery.ProductsQuantity += context.Message.ProductsQuantity;
            }
            catch (NullDataException)
            {
                packToDelivery = await AddNewPackToDelivery(context.Message.OrderId, context.Message.ProductsQuantity);
            }
                          
            await packToDeliveryRepo.SaveAllAsync();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task<PackToDelivery> AddNewPackToDelivery(int orderId, int productsQuantity)
        {
            var order = await customerOrderService.GetOrder(orderId);
            var packToDelivery = new PackToDelivery
            {
                OrderId = orderId,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                ProductsQuantity = productsQuantity,
                PackStatus = PackStatus.Waiting
            };

            packToDeliveryRepo.Add(packToDelivery);

            return packToDelivery;
        }
    }
}
