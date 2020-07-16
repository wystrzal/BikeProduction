using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Warehouse.Application.Commands.Handlers
{
    public class AddPartCommandHandler : IRequestHandler<AddPartCommand>
    {
        public AddPartCommandHandler()
        {

        }
        public Task<Unit> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
