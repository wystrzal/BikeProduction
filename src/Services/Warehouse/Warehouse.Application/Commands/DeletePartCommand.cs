using MediatR;

namespace Warehouse.Application.Commands
{
    public class DeletePartCommand : IRequest
    {
        public int PartId { get; set; }

        public DeletePartCommand(int partId)
        {
            PartId = partId;
        }
    }
}
