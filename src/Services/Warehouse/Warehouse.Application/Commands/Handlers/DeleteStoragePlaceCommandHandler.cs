using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Exceptions;
using Warehouse.Core.Interfaces;

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

            if (storagePlace == null)
                throw new StoragePlaceNotFoundException();

            storagePlaceRepo.Delete(storagePlace);

            await storagePlaceRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}
