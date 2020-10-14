using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Commands.Handlers
{
    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand>
    {
        private readonly IPartRepository partRepository;

        public DeletePartCommandHandler(IPartRepository partRepository)
        {
            this.partRepository = partRepository;
        }

        public async Task<Unit> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            var part = await partRepository.GetById(request.PartId);

            await DeletePart(part);

            return Unit.Value;
        }

        private async Task DeletePart(Part part)
        {
            partRepository.Delete(part);
            await partRepository.SaveAllAsync();
        }
    }
}
