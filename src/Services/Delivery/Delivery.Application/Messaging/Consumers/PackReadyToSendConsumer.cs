using Common.Application.Commands;
using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Application.Messaging.MessagingModels.OrderStatusEnum;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Messaging.Consumers
{
    public class PackReadyToSendConsumer : IConsumer<PackReadyToSendEvent>
    {
        private readonly IPackToDeliveryRepo packToDeliveryRepo;
        private readonly IBus bus;

        public PackReadyToSendConsumer(IPackToDeliveryRepo packToDeliveryRepo, IBus bus)
        {
            this.packToDeliveryRepo = packToDeliveryRepo;
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<PackReadyToSendEvent> context)
        {
            var pack = await packToDeliveryRepo.GetByConditionFirst(x => x.OrderId == context.Message.OrderId);

            pack.PackStatus = PackStatus.ReadyToSend;

            await packToDeliveryRepo.SaveAllAsync();

            await bus.Publish(new ChangeOrderStatusEvent(context.Message.OrderId, OrderStatus.ReadyToSend));
        }
    }
}
