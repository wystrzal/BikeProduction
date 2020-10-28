using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Commands.Handlers
{
    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand>
    {
        private readonly IPartRepository partRepository;
        private readonly IMapper mapper;

        public UpdatePartCommandHandler(IPartRepository partRepository, IMapper mapper)
        {
            this.partRepository = partRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var part = await partRepository.GetById(request.Id);

            mapper.Map(request, part);

            await partRepository.SaveAllAsync();

            return Unit.Value;
        }
    }
}
