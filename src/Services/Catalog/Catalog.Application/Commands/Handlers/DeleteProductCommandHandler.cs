using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IBus bus;

        public DeleteProductCommandHandler(IProductRepository productRepository, IBus bus)
        {
            this.productRepository = productRepository;
            this.bus = bus;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            string reference = product.Reference;

            if (await productRepository.SaveAllAsync())
            {
                await bus.Publish(new ProductDeletedEvent(reference));
            }

            return Unit.Value;
        }
    }
}
