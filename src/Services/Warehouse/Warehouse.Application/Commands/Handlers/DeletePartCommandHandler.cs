using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;

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

            partRepository.Delete(part);

            await partRepository.SaveAllAsync();

            return Unit.Value;
        }
    }
}
