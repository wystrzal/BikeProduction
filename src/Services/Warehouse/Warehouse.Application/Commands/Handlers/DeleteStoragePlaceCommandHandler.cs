using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Commands.Handlers
{
    public class DeleteStoragePlaceCommandHandler : IRequestHandler<DeleteStoragePlaceCommand>
    {
        private readonly IStoragePlaceRepo storagePlaceRepo;

        public DeleteStoragePlaceCommandHandler(IStoragePlaceRepo storagePlaceRepo)
        {
            this.storagePlaceRepo = storagePlaceRepo;
        }

        public async Task<Unit> Handle(DeleteStoragePlaceCommand request, CancellationToken cancellationToken)
        {
            var storagePlace = await storagePlaceRepo.GetById(request.StoragePlaceId);

            await DeleteStoragePlace(storagePlace);

            return Unit.Value;
        }

        private async Task DeleteStoragePlace(StoragePlace storagePlace)
        {
            storagePlaceRepo.Delete(storagePlace);
            await storagePlaceRepo.SaveAllAsync();
        }
    }
}
