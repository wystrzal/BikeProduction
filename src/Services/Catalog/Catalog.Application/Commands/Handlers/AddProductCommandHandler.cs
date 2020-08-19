using AutoMapper;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IBus bus;

        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper, IBus bus)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.bus = bus;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productToAdd = mapper.Map<Product>(request);

            productRepository.Add(productToAdd);

            if (await productRepository.SaveAllAsync())
            {
                await bus.Publish(new ProductAddedEvent(request.ProductName, request.Reference));
            }

            return Unit.Value;
        }
    }
}
