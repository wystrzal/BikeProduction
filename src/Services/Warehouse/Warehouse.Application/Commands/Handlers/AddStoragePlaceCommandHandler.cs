using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Commands.Handlers
{
    public class AddStoragePlaceCommandHandler : IRequestHandler<AddStoragePlaceCommand>
    {
        private readonly IStoragePlaceRepo storagePlaceRepo;
        private readonly IMapper mapper;

        public AddStoragePlaceCommandHandler(IStoragePlaceRepo storagePlaceRepo, IMapper mapper)
        {
            this.storagePlaceRepo = storagePlaceRepo;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(AddStoragePlaceCommand request, CancellationToken cancellationToken)
        {
            var storagePlaceToAdd = mapper.Map<StoragePlace>(request);

            storagePlaceRepo.Add(storagePlaceToAdd);

            await storagePlaceRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}
