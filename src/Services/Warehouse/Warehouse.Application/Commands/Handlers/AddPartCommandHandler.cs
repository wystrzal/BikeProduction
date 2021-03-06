﻿using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Commands.Handlers
{
    public class AddPartCommandHandler : IRequestHandler<AddPartCommand>
    {
        private readonly IPartRepository partRepository;
        private readonly IMapper mapper;

        public AddPartCommandHandler(IPartRepository partRepository, IMapper mapper)
        {
            this.partRepository = partRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            var partToAdd = mapper.Map<Part>(request);

            await AddPart(partToAdd);

            return Unit.Value;
        }

        private async Task AddPart(Part partToAdd)
        {
            partRepository.Add(partToAdd);
            await partRepository.SaveAllAsync();
        }
    }
}
