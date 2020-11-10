using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Application.Commands.Handlers
{
    public class DeleteLoadingPlaceCommandHandler : IRequestHandler<DeleteLoadingPlaceCommand>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;

        public DeleteLoadingPlaceCommandHandler(ILoadingPlaceRepo loadingPlaceRepo)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
        }

        public async Task<Unit> Handle(DeleteLoadingPlaceCommand request, CancellationToken cancellationToken)
        {
            var loadingPlace = await loadingPlaceRepo
               .GetByConditionWithIncludeFirst(x => x.Id == request.LoadingPlaceId, y => y.PacksToDelivery);

            CheckIfHaveAnyPacks(loadingPlace);

            await DeleteLoadingPlace(loadingPlace);

            return Unit.Value;
        }
        private void CheckIfHaveAnyPacks(LoadingPlace loadingPlace)
        {
            if (loadingPlace.PacksToDelivery.Count > 0)
            {
                throw new ArgumentException("The loading place could not be removed, because packs were loaded.");
            }
        }

        private async Task DeleteLoadingPlace(LoadingPlace loadingPlace)
        {
            loadingPlaceRepo.Delete(loadingPlace);
            await loadingPlaceRepo.SaveAllAsync();
        }
    }
}
