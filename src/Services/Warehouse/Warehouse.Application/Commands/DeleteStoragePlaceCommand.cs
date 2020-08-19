using MediatR;

namespace Warehouse.Application.Commands
{
    public class DeleteStoragePlaceCommand : IRequest
    {
        public int StoragePlaceId { get; set; }

        public DeleteStoragePlaceCommand(int storagePlaceId)
        {
            StoragePlaceId = storagePlaceId;
        }
    }
}
