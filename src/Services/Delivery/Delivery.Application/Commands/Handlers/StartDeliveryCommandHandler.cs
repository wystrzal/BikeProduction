﻿using Common.Application.Messaging;
using Delivery.Core.Interfaces;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Delivery.Application.Messaging.MessagingModels.OrderStatusEnum;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Commands.Handlers
{
    public class StartDeliveryCommandHandler : IRequestHandler<StartDeliveryCommand>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IBus bus;

        public StartDeliveryCommandHandler(ILoadingPlaceRepo loadingPlaceRepo, IBus bus)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.bus = bus;
        }

        public async Task<Unit> Handle(StartDeliveryCommand request, CancellationToken cancellationToken)
        {
            var loadingPlace = await loadingPlaceRepo
                .GetByConditionWithIncludeFirst(x => x.Id == request.LoadingPlaceId, y => y.PacksToDelivery);

            foreach (var pack in loadingPlace.PacksToDelivery)
            {
                pack.PackStatus = PackStatus.Sended;

                await bus.Publish(new ChangeOrderStatusEvent(pack.OrderId, OrderStatus.Sended));
            }

            loadingPlace.LoadingPlaceStatus = LoadingPlaceStatus.Sended;

            await loadingPlaceRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}