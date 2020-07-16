using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Delivery.Application.Messaging.MessagingModels.OrderStatusEnum;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Commands.Handlers
{
    public class CompleteDeliveryCommandHandler : IRequestHandler<CompleteDeliveryCommand>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IBus bus;

        public CompleteDeliveryCommandHandler(ILoadingPlaceRepo loadingPlaceRepo, IBus bus)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.bus = bus;
        }

        public async Task<Unit> Handle(CompleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            var loadingPlace = await loadingPlaceRepo
                .GetByConditionWithIncludeFirst(x => x.Id == request.LoadingPlaceId, y => y.PacksToDelivery);

            foreach (var pack in loadingPlace.PacksToDelivery)
            {
                pack.LoadingPlace = null;
                pack.PackStatus = PackStatus.Delivered;

                await bus.Publish(new ChangeOrderStatusEvent(pack.OrderId, OrderStatus.Delivered));
            }

            loadingPlace.LoadedQuantity = 0;
            loadingPlace.LoadingPlaceStatus = LoadingPlaceStatus.WaitingForLoading;

            await loadingPlaceRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}
