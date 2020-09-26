using Common.Application.Commands;
using Common.Application.Messaging;
using Delivery.Core.Interfaces;
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
                pack.PackStatus = PackStatus.ReadyToSend;
                await packToDeliveryRepo.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            await bus.Publish(new ChangeOrderStatusEvent(context.Message.OrderId, OrderStatus.ReadyToSend));

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}
