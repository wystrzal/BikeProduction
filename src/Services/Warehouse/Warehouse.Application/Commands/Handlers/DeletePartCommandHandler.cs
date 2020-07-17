﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Core.Exceptions;
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

            if (part == null)
            {
                throw new PartNotFoundException();
            }

            partRepository.Delete(part);

            await partRepository.SaveAllAsync();

            return Unit.Value;
        }
    }
}
