using AutoMapper;
using BikeBaseRepository;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IBus bus;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IBus bus)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.bus = bus;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);
            var oldReference = product.Reference;

            bool productChanged = product.ProductName != request.ProductName || oldReference != request.Reference;

            mapper.Map(request, product);

            try
            {
                await productRepository.SaveAllAsync();
            }
            catch (ChangesNotSavedCorrectlyException)
            {
                return Unit.Value;
            }
            
            if (productChanged)
            {
                await bus.Publish(new ProductUpdatedEvent(product.ProductName, product.Reference, oldReference));
            }

            return Unit.Value;
        }
    }
}
