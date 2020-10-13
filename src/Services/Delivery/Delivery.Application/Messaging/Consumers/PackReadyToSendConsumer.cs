using Common.Application.Commands;
using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.PackStatusEnum;
using static Delivery.Core.Models.MessagingModels.OrderStatusEnum;

namespace Delivery.Application.Messaging.Consumers
{
    public class PackReadyToSendConsumer : IConsumer<PackReadyToSendEvent>
    {
        private readonly IPackToDeliveryRepo packToDeliveryRepo;
        private readonly IBus bus;
        private readonly ILogger<PackReadyToSendConsumer> logger;

        public PackReadyToSendConsumer(IPackToDeliveryRepo packToDeliveryRepo, IBus bus, ILogger<PackReadyToSendConsumer> logger)
        {
            this.packToDeliveryRepo = packToDeliveryRepo;
            this.bus = bus;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<PackReadyToSendEvent> context)
        {
            try
            {
                var pack = await packToDeliveryRepo.GetByConditionFirst(x => x.OrderId == context.Message.OrderId);
                await ChangePackStatusToReadyToSend(pack, context.Message.OrderId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }      

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task ChangePackStatusToReadyToSend(PackToDelivery pack, int orderId)
        {
            pack.PackStatus = PackStatus.ReadyToSend;
            await packToDeliveryRepo.SaveAllAsync();
            await bus.Publish(new ChangeOrderStatusEvent(orderId, OrderStatus.ReadyToSend));
        }
    }
}
