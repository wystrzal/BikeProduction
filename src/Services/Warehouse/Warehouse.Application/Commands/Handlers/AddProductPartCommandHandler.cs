using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Commands.Handlers
{
    public class AddProductPartCommandHandler : IRequestHandler<AddProductPartCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductPartRepo productPartRepo;

        public AddProductPartCommandHandler(IProductRepository productRepository, IProductPartRepo productPartRepo)
        {
            this.productRepository = productRepository;
            this.productPartRepo = productPartRepo;
        }

        public async Task<Unit> Handle(AddProductPartCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByConditionFirst(x => x.Reference == request.Reference);

            await AddNewProductPart(request.PartId, product.Id);

            return Unit.Value;
        }

        private async Task AddNewProductPart(int partId, int productId)
        {
            var productPart = new ProductsParts
            {
                PartId = partId,
                ProductId = productId
            };

            productPartRepo.Add(productPart);
            await productPartRepo.SaveAllAsync();
        }
    }
}
