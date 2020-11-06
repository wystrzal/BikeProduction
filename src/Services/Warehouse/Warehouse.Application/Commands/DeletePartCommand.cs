using MediatR;
using System;

namespace Warehouse.Application.Commands
{
    public class DeletePartCommand : PartIdCommand
    {
        public DeletePartCommand(int partId) : base(partId)
        {
        }
    }
}
