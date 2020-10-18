using Common.Application.Messaging;
using Delivery.Core.Exceptions;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;
using static Delivery.Core.Models.Enums.PackStatusEnum;
using static Delivery.Core.Models.MessagingModels.OrderStatusEnum;

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

            await ChangePacksStatusToDelivered(loadingPlace);

            ClearLoadedQuantityFromLoadingPlace(loadingPlace);

            await loadingPlaceRepo.SaveAllAsync();

            return Unit.Value;
        }

        private async Task ChangePacksStatusToDelivered(LoadingPlace loadingPlace)
        {
            foreach (var pack in loadingPlace.PacksToDelivery)
            {
                pack.LoadingPlace = null;
                pack.PackStatus = PackStatus.Delivered;

                await bus.Publish(new ChangeOrderStatusEvent(pack.OrderId, OrderStatus.Delivered));
            }
        }

        private void ClearLoadedQuantityFromLoadingPlace(LoadingPlace loadingPlace)
        {
            loadingPlace.LoadedQuantity = 0;
            loadingPlace.LoadingPlaceStatus = LoadingPlaceStatus.Waiting_For_Loading;
        }
    }
}
