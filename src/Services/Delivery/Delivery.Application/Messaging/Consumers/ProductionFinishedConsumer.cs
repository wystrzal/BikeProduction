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
            try
            {
                var packToDelivery = await packToDeliveryRepo.GetByConditionFirst(x => x.OrderId == context.Message.OrderId);
                packToDelivery.ProductsQuantity += context.Message.ProductsQuantity;
            }
            catch (NullDataException)
            {
                await CreateNewPackToDelivery(context.Message.OrderId, context.Message.ProductsQuantity);
            }

            await SaveChanges();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task CreateNewPackToDelivery(int orderId, int productsQuantity)
        {
            var order = await GetOrder(orderId);

            var packToDelivery = new PackToDelivery
            {
                OrderId = orderId,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                ProductsQuantity = productsQuantity,
                PackStatus = PackStatus.Waiting
            };

            packToDeliveryRepo.Add(packToDelivery);
        }

        private async Task<Order> GetOrder(int orderId)
        {
            try
            {
                return await customerOrderService.GetOrder(orderId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task SaveChanges()
        {
            try
            {
                await packToDeliveryRepo.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
