using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Commands.Handlers
{
    public class DeleteProductPartCommandHandler : IRequestHandler<DeleteProductPartCommand>
    {
        private readonly IProductPartRepo productPartRepo;

        public DeleteProductPartCommandHandler(IProductPartRepo productPartRepo)
        {
            this.productPartRepo = productPartRepo;
        }

        public async Task<Unit> Handle(DeleteProductPartCommand request, CancellationToken cancellationToken)
        {
            var productPart = await productPartRepo.GetProductPart(request.Reference, request.PartId);

            productPartRepo.Delete(productPart);

            await productPartRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}
