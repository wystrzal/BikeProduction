using Common.Application.Commands;
using Delivery.Core.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Messaging.Consumers
{
    public class PackReadyToSendConsumer : IConsumer<PackReadyToSendEvent>
    {
        private readonly IPackToDeliveryRepo packToDeliveryRepo;

        public PackReadyToSendConsumer(IPackToDeliveryRepo packToDeliveryRepo)
        {
            this.packToDeliveryRepo = packToDeliveryRepo;
        }

        public async Task Consume(ConsumeContext<PackReadyToSendEvent> context)
        {
            var packs = await packToDeliveryRepo.GetByConditionToList(x => x.OrderId == context.Message.OrderId);

            foreach (var pack in packs)
            {
                pack.PackStatus = PackStatus.ReadyToSend;
            }

            await packToDeliveryRepo.SaveAllAsync();
        }
    }
}
