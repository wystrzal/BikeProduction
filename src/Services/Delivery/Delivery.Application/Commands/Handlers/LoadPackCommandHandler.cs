using Common.Application.Messaging;
using Delivery.Core.Exceptions;
using Delivery.Core.Interfaces;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Delivery.Application.Messaging.MessagingModels.OrderStatusEnum;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Application.Commands.Handlers
{
    public class LoadPackCommandHandler : IRequestHandler<LoadPackCommand>
    {
        private readonly IPackToDeliveryRepo packToDeliveryRepo;
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IBus bus;

        public LoadPackCommandHandler(IPackToDeliveryRepo packToDeliveryRepo, ILoadingPlaceRepo loadingPlaceRepo, IBus bus)
        {
            this.packToDeliveryRepo = packToDeliveryRepo;
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.bus = bus;
        }

        public async Task<Unit> Handle(LoadPackCommand request, CancellationToken cancellationToken)
        {
            var pack = await packToDeliveryRepo.GetByConditionFirst(x => x.Id == request.PackId);

            if (pack == null)
            {
                throw new PackNotFoundException();
            }

            var loadingPlace = await loadingPlaceRepo.GetByConditionFirst(x => x.Id == request.LoadingPlaceId);

            if (loadingPlace == null)
            {
                throw new LoadingPlaceNotFoundException();
            }

            if (pack.ProductsQuantity > (loadingPlace.AmountOfSpace - loadingPlace.LoadedQuantity))
            {
                throw new LackOfSpaceException();
            }

            pack.LoadingPlace = loadingPlace;
            pack.PackStatus = PackStatus.ReadyToSend;

            loadingPlace.LoadedQuantity += pack.ProductsQuantity;
            loadingPlace.LoadingPlaceStatus = LoadingPlaceStatus.Loading;

            await loadingPlaceRepo.SaveAllAsync();

            await bus.Publish(new ChangeOrderStatusEvent(pack.OrderId, OrderStatus.ReadyToSend));

            return Unit.Value;
        }
    }
}
