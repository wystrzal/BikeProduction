using AutoMapper;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using System;
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

            productToAdd.Reference = await GenerateReference(productToAdd.Reference);

            await AddProduct(productToAdd);

            await bus.Publish(new ProductAddedEvent(request.ProductName, request.Reference));

            return Unit.Value;
        }

        private async Task<string> GenerateReference(string reference)
        {
            var random = new Random();

            while (await productRepository.CheckIfExistByCondition(x => x.Reference == reference))
            {
                reference = random.Next(1, 999999).ToString();
            }

            return reference;
        }

        private async Task AddProduct(Product productToAdd)
        {
            productRepository.Add(productToAdd);
            await productRepository.SaveAllAsync();
        }
    }
}
