using BaseRepository.Exceptions;
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
                await CreateNewPackToDelivery(context);
            }

            await SaveChanges();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task CreateNewPackToDelivery(ConsumeContext<ProductionFinishedEvent> context)
        {
            var order = await GetOrder(context.Message.OrderId, context.Message.Token);

            var packToDelivery = new PackToDelivery
            {
                OrderId = context.Message.OrderId,
                PostCode = order.PostCode,
                City = order.City,
                Street = order.Street,
                HouseNumber = order.HouseNumber,
                PhoneNumber = order.PhoneNumber,
                ProductsQuantity = context.Message.ProductsQuantity,
                PackStatus = PackStatus.Waiting
            };

            packToDeliveryRepo.Add(packToDelivery);
        }

        private async Task<Order> GetOrder(int orderId, string token)
        {
            try
            {
                return await customerOrderService.GetOrder(orderId, token);
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
